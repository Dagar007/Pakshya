using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Interests
{
    public class List
    {
        public class Query : IRequest<List<UserInterestDto>>
        {
        }
        public class Handler : IRequestHandler<Query, List<UserInterestDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<UserInterestDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var allInterests = await _context.Categories.Where(x => x.IsActive).ToListAsync();
                return _mapper.Map<List<Category>, List<UserInterestDto>>(allInterests);  
            }
        }
    }
}