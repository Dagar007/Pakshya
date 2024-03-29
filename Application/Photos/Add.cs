using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos
{
    public class Add
    {
        public class Command : IRequest<Photo>
        {
            public IFormFile File { get; set; }
        }


        public class Handler : IRequestHandler<Command, Photo>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IImageService _photoAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor, IImageService photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Photo> Handle(Command request, CancellationToken cancellationToken)
            {
                var photoUploadResult = await _photoAccessor.UploadFileAsync("pakshya.bucket",request.File);
                var user = await _context.Users.SingleOrDefaultAsync(x=> x.Email == _userAccessor.GetEmail(), cancellationToken: cancellationToken);

                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.Key
                };

                if(!user.Photos.Any(x => x.IsMain))
                    photo.IsMain = true;
                
                user.Photos.Add(photo);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success) return photo;

                throw new Exception("Error saving changes");
            }
        }
    }
}