using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class Edit
    {
        public class Command : IRequest
        {
            public string DisplayName { get; set; }
            public string Bio { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Bio.Length).GreaterThan(10);
            }
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
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUserName());
                user.Bio = request.Bio ?? user.Bio;
                user.DisplayName = request.DisplayName ?? user.DisplayName;
                // var post = await _context.Posts.FindAsync(request.Id);
                // if (post == null)
                //     throw new RestException(HttpStatusCode.NotFound, new { post = "Not found" });
                // post.Heading = request.Heading ?? post.Heading;
                // post.Description = request.Description ?? post.Description;
                // post.Category = request.Category ?? post.Category;
                // post.Date = request.Date ?? post.Date;
                // post.Url = request.Url ?? post.Url;


                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception($"Problem saving profile.");
            }
        }
    }
}