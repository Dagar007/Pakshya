using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.User
{
    public class CurrentUser
    {
        public class CurrentUserQuery : IRequest<User>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<CurrentUserQuery, User>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenertor;
            private readonly IUserAccessor _userAccessor;

            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenertor, IUserAccessor userAccessor)
            {
                _userManager = userManager;
                _jwtGenertor = jwtGenertor;
                _userAccessor = userAccessor;
            }

            public async Task<User> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
            {
               var user = await _userManager.FindByEmailAsync(_userAccessor.GetEmail());
               return new User {
                   DisplayName = user.DisplayName,
                   Username = user.UserName,
                   RefreshToken = _jwtGenertor.GenerateRefreshToken(),
                   Token = _jwtGenertor.CreateToken(user),
                   Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
               };
            }
        }
    }
}