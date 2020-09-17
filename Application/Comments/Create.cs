using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments
{
    public class Create
    {
        public class Command : IRequest<CommentDto>
        {
            public string Body { get; set; }
            public Guid PostId { get; set; }
            public string Username { get; set; }
            public bool For { get; set; }
            public bool Against { get; set; }   
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {

            }
        }

        public class Handler : IRequestHandler<Command, CommentDto>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _mapper = mapper;
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<CommentDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.FindAsync(request.PostId);
                if(post == null)
                    throw new RestException(HttpStatusCode.NotFound, new {Post = "Not Found"});
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == request.Username);

                var comment = new Comment 
                {
                    Author = user,
                    Post = post,
                    Body = request.Body,
                    Date = DateTime.Now,
                    For = request.For,
                    Against = request.Against,
                    IsActive = true,
                };

                post.Comments.Add(comment);

                 var isCommentAuthor = new UserCommentLike
                {
                    AppUser = user,
                    Comment = comment,
                    IsAuthor = true,
                    IsLiked = false
                };
                _context.UserCommentLikes.Add(isCommentAuthor);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return _mapper.Map<CommentDto>(comment);

                throw new Exception("problem saving new post.");
            }
        }
    }
}