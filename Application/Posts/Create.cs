using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence;

namespace Application.Posts
{
    public class Create
    {
        public class Command : IRequest
        {
            public IFormFile File { get; set; }
            public string JsonPost { get; set; }

        }

        public class JsonPostDeseroalized 
        {
            public Guid Id { get; set; }
            public string Heading { get; set; }
            public string Description { get; set; }
            public Category Category { get; set; }
            public DateTime Date { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IPhotoS3Accessor _photoAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor, IPhotoS3Accessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var jsonPostDeseroalized = JsonConvert.DeserializeObject<JsonPostDeseroalized>(request.JsonPost);
                var category = await _context.Categories.FindAsync(jsonPostDeseroalized.Category.Id);
                if (category == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Category = "Not found" });
                var post = new Post
                {
                    Id = jsonPostDeseroalized.Id,
                    Heading = jsonPostDeseroalized.Heading,
                    Description = jsonPostDeseroalized.Description,
                    Category = category,
                    Date = DateTime.Now,
                    For = 0,
                    Against = 0,
                    Photos = new List<Photo>(),
                    IsActive = true,
                    
                };
                if (request.File != null && request.File.Length > 0)
                {
                    var photoUploadResult = await _photoAccessor.UploadFileAsync("pakshya.bucket",request.File);
                    var photo = new Photo
                    {
                        Url = photoUploadResult.Url,
                        Id = photoUploadResult.Key
                    };
                    post.Photos.Add(photo);
                }
                _context.Posts.Add(post);
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());

                // Will remove the following as we don't want user liking there own posts.
                // var isHost = new UserPostLike
                // {
                //     AppUser = user,
                //     Post = post,
                //     IsAuthor = true
                // };
                // _context.UserPostLikes.Add(isHost);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("problem saving new post.");
            }
        }
    }
}