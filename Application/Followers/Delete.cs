using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Followers
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Email { get; set; }
        }


        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var observer = await _context.Users.SingleOrDefaultAsync(x => x.Email == _userAccessor.GetEmail(), cancellationToken: cancellationToken);

                var target = await _context.Users.SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken: cancellationToken);
                if (target == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not Found" });
                var following = await _context.Followings
                    .SingleOrDefaultAsync(x => x.ObserverId == observer.Id && x.TargetId == target.Id, cancellationToken: cancellationToken);

                if (following == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { User = "You are not following this user" });

                _context.Followings.Remove(following);


                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem deleting the followers");
            }
        }
    }
}