using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Interests
{
    public class ListUserInterests
    {
        public class UserInterestsQuery : IRequest<List<Guid>>
        {
            public string Id { get; set; }
        }
        public class Handler : IRequestHandler<UserInterestsQuery, List<Guid>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Guid>> Handle(UserInterestsQuery request, CancellationToken cancellationToken)
            {
                var allInterests = await _context.UserInterests.Where(x => x.AppUserId == request.Id).ToListAsync(cancellationToken: cancellationToken);
                return  allInterests.Select(x => x.CategoryId).ToList();
            }
        }
    }
}
