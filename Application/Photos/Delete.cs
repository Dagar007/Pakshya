using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Id { get; set; }
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
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == _userAccessor.GetEmail(), cancellationToken: cancellationToken);
                var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);
                if (photo == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Photo = "Not Found" });
                if (photo.IsMain)
                    throw new RestException(HttpStatusCode.BadRequest, new { Photo = "Can't delete main photo." });

                await _photoAccessor.DeletePhoto(request.Id,"pakshya.bucket");
                user.Photos.Remove(photo);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success) return Unit.Value;

                throw new Exception("Error deleting photo");
            }
        }
    }
}