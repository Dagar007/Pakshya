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
            private readonly IBlobService _blobService;
            public Handler(DataContext context, IUserAccessor userAccessor, IBlobService blobService)
            {
                _blobService = blobService;
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
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
                if (request.File is { Length: > 0 })
                {
                    var photoUploadResult = await _blobService.UploadContentBlobAsync(request.File,request.File.FileName);
                    var photo = new Photo
                    {
                        Url = photoUploadResult.Url,
                        Id = photoUploadResult.Id
                    };
                    post.Photos.Add(photo);
                }
                await _context.Posts.AddAsync(post, cancellationToken);
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == _userAccessor.GetEmail(), cancellationToken: cancellationToken);

                var isHost = new UserPostLike
                {
                    AppUser = user,
                    Post = post,
                    IsAuthor = true,
                    IsLiked = false
                };
                await _context.UserPostLikes.AddAsync(isHost, cancellationToken);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success) return Unit.Value;

                throw new Exception("problem saving new post.");
            }
        }
    }
}