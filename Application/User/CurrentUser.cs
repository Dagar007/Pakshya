using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserAccessor _userAccessor;

            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _userAccessor = userAccessor;
            }

            public async Task<User> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
            {
               var user = await _userManager.FindByEmailAsync(_userAccessor.GetEmail());
               return new User {
                   DisplayName = user.DisplayName,
                   Id = user.Id,
                   RefreshToken = _jwtGenerator.GenerateRefreshToken(),
                   Token = _jwtGenerator.CreateToken(user),
                   Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
               };
            }
        }
    }
}