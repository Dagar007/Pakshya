using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security
{
    public class IsHostRequirement : IAuthorizationRequirement
    {
    }
    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public IsHostRequirementHandler(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext
            .User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var postId = Guid.Parse(_httpContextAccessor.HttpContext.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value.ToString());

            var post = _context.Posts.FindAsync(postId).Result;
            var host = post.UserPostLikes.FirstOrDefault(x => x.IsAuthor);
            if (host?.AppUser?.Email == currentUserEmail)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}