using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security
{
    public class IsHostRequirementForComment : IAuthorizationRequirement
    {
    }

    public class IsHostRequirementHandlerComment : AuthorizationHandler<IsHostRequirementForComment>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public IsHostRequirementHandlerComment(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirementForComment requirement)
        {
            var currentUserName = _httpContextAccessor.HttpContext
            .User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var commentId = Guid.Parse(_httpContextAccessor.HttpContext.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value.ToString());

            var comment = _context.Comments.FindAsync(commentId).Result;
            var host = comment.Author.UserName;
            if (host == currentUserName)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}

