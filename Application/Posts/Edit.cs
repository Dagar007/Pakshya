using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Application.Posts
{
    public class Edit
    {
        public class Command : IRequest<Unit>
        {
            public Guid Id { get; set; }
            public string Heading { get; set; }
            public string Description { get; set; }
            public Guid CategoryId { get; set; }
            public IFormFile File { get; set; }
            public string IsImageEdited { get; set; }
        }


        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Heading).NotEmpty().WithMessage("Heading of post can't be empty");
                RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category of post can't be empty");
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext _context;
            private readonly IBlobService _blobService;
            public Handler(DataContext context, IBlobService blobService)
            {
                _blobService = blobService;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var post = await _context.Posts.FindAsync(request.Id);
                if (post == null)
                    throw new RestException(HttpStatusCode.NotFound, new { post = "Not found" });

                var category = await _context.Categories.FindAsync(request.CategoryId);
                if (category == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Category = "Not found" });

                post.Category = category;
                post.Heading = request.Heading ?? post.Heading;
                post.Description = request.Description ?? post.Description;
                post.IsActive = true;
                post.Date = DateTime.Now;

                // Remove all posts photos
                if (request.IsImageEdited == "y")
                {
                    var photos = post.Photos;
                    foreach (var ph in photos)
                    {
                        await _blobService.DeleteBlobAsync(ph.Url.Split("/").Last());
                    }
                    _context.Photos.RemoveRange(photos);

                    if (request.File is { Length: > 0 })
                    {
                        // Start fresh upload
                        var photoUploadResult = await _blobService.UploadContentBlobAsync(request.File, request.File.FileName);
                        var photo = new Photo
                        {
                            Url = photoUploadResult.Url,
                            Id = photoUploadResult.Id
                        };
                        post.Photos.Add(photo);
                    }
                }

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success) return Unit.Value;

                throw new Exception($"Problem saving {request.Id} post");
            }
        }
    }
}