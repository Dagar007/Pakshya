using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Interests
{
    public class AddUserInterestsCommand : IRequest
    {
        public List<Guid> Ids { get; set; }
    }
    public class Handler : IRequestHandler<AddUserInterestsCommand>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<Unit> Handle(AddUserInterestsCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == _userAccessor.GetEmail(), cancellationToken: cancellationToken);
            if (_context.UserInterests.Any(x => x.AppUser.Id == user.Id))
                _context.UserInterests.RemoveRange(_context.UserInterests.Where(x => x.AppUser == user));
            foreach (var interest in request.Ids)
            {
                if (!_context.Categories.Any(x => x.Id == interest))
                    throw new RestException(HttpStatusCode.NotFound, new { Interest = "Not found" });
                var interestToAdd = new UserInterest
                {
                    AppUser = user,
                    CategoryId = interest
                };
                await _context.UserInterests.AddAsync(interestToAdd, cancellationToken);
            }
            var success = await _context.SaveChangesAsync(cancellationToken) > 0;
            if (success) return Unit.Value;

            throw new Exception("problem saving new post.");
        }
    }
}