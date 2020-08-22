using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
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
            public Address Address { get; set; }
            public string Education { get; set; }
            public string Work { get; set; }
            public List<InterestDTO> Interests { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
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

                StringBuilder sb = new StringBuilder();
                foreach (var i in request.Interests)
                {
                    if(i.DoesUser)
                    {
                       if(sb.Length == 0)
                       {
                           sb.Append(i.Id);
                       }
                       else {
                           sb.Append(',').Append(i.Id);
                       }
                    }
                }
                user.Bio = request.Bio ?? user.Bio;
                user.DisplayName = request.DisplayName ?? user.DisplayName;
                user.Address = request.Address ?? user.Address;
                user.Education = request.Education ?? user.Education;
                user.Work = request.Work ?? user.Work;
                //user.Interests = sb.ToString();




                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception($"Problem saving profile.");
            }
        }
    }
}