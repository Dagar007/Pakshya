using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class List
    {
        public class Query : IRequest<List<PostDto>> { }
        public class Handler : IRequestHandler<Query, List<PostDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<PostDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var posts = await _context.Posts
                .ToListAsync();
                //var postToReturn = _mapper.Map<Post, PostDto>(post);
                return _mapper.Map<List<Post>,List<PostDto>>(posts);
            }
        }
    }
}