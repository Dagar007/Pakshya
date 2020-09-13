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
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, IFormFile file, string jsonPost)
            {
                Id = id;
                File = file;
                JsonPost = jsonPost;
                this.jsonPostDeserialized = JsonConvert.DeserializeObject<JsonPostDeseroalized>(jsonPost);
            }

            public Guid Id { get; private set; }
            public IFormFile File { get; private set; }
            public string JsonPost { get; private set; }
            public JsonPostDeseroalized jsonPostDeserialized { get; private set; }
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
        public class CommandValidator : AbstractValidator<JsonPostDeseroalized>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Heading).NotEmpty().WithMessage("Heading of post can't be empty");
                RuleFor(x => x.Category).NotEmpty().WithMessage("Category of post can't be empty"); 
            }
        }

        public class Handler : IRequestHandler<Command,Unit>
        {
            private readonly DataContext _context;
            private readonly IPhotoS3Accessor _photoAccessor;
            public Handler(DataContext context, IPhotoS3Accessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var post = await _context.Posts.FindAsync(request.Id);
                if (post == null)
                    throw new RestException(HttpStatusCode.NotFound, new { post = "Not found" });
                var jsonPostDeseroalized = request.jsonPostDeserialized;
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
                        await _photoAccessor.DeletePhoto(ph.Id, "pakshya.bucket");
                    }
                    _context.Photos.RemoveRange(photos);

                    if (request.File != null && request.File.Length > 0)
                    {
                        // Start fresh upload
                        var photoUploadResult = await _photoAccessor.UploadFileAsync("pakshya.bucket",request.File);
                        var photo = new Photo
                        {
                            Url = photoUploadResult.Url,
                            Id = photoUploadResult.Key
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