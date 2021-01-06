using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain;
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
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context,IUserAccessor userAccessor, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<List<Guid>> Handle(UserInterestsQuery request, CancellationToken cancellationToken)
            {
                var allInterests = await _context.UserInterests.Where(x => x.AppUserId == request.Id).ToListAsync();
                return  allInterests.Select(x => x.CategoryId).ToList();
            }
        }
    }
}
