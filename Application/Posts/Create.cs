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
            public Guid Id { get; set; }
            public string Heading { get; set; }
            public string Description { get; set; }
            public Guid CategoryId { get; set; }
            public IFormFile File { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Heading).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
            }
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
                //var jsonPostDeseroalized = JsonConvert.DeserializeObject<JsonPostDeseroalized>(request.JsonPost);
                var category = await _context.Categories.FindAsync(request.CategoryId);
                if (category == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Category = "Not found" });
                var post = new Post
                {
                    Id = request.Id,
                    Heading = request.Heading,
                    Description = request.Description,
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
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == _userAccessor.GetEmail());

                var isHost = new UserPostLike
                {
                    AppUser = user,
                    Post = post,
                    IsAuthor = true,
                    IsLiked = false
                };
                _context.UserPostLikes.Add(isHost);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("problem saving new post.");
            }
        }
    }
}