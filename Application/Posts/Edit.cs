using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Persistence;

namespace Application.Posts
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
            public bool IsImageEdited { get; set; }

        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            public Handler(DataContext context, IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var post = await _context.Posts.FindAsync(request.Id);
                if (post == null)
                    throw new RestException(HttpStatusCode.NotFound, new { post = "Not found" });
                var jsonPostDeseroalized = JsonConvert.DeserializeObject<JsonPostDeseroalized>(request.JsonPost);
                if (jsonPostDeseroalized.Category != null)
                {
                    var category = await _context.Categories.FindAsync(jsonPostDeseroalized.Category.Id);
                    if (category == null)
                        throw new RestException(HttpStatusCode.NotFound, new { Category = "Not found" });
                    post.Category = category;
                }

                post.Heading = jsonPostDeseroalized.Heading ?? post.Heading;
                post.Description = jsonPostDeseroalized.Description ?? post.Description;
                post.IsActive = true;

                post.Date = DateTime.Now;

                // Remove all posts photos
                if (jsonPostDeseroalized.IsImageEdited)
                {
                    var photos = post.Photos;
                    foreach (var ph in photos)
                    {
                        _photoAccessor.DeletePhoto(ph.Id);
                    }
                    _context.Photos.RemoveRange(photos);

                    if (request.File != null && request.File.Length > 0)
                    {
                        // Start fresh upload
                        var photoUploadResult = _photoAccessor.AddPhoto(request.File);
                        var photo = new Photo
                        {
                            Url = photoUploadResult.Url,
                            Id = photoUploadResult.PublicId
                        };
                        post.Photos.Add(photo);
                    }
                }

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception($"Problem saving {jsonPostDeseroalized.Id} post");
            }
        }
    }
}