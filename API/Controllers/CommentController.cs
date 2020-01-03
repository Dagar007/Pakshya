using System;
using System.Threading.Tasks;
using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CommentController : BaseController
    {
        [HttpDelete("{id}")]
        [Authorize(Policy = "IsCommentHost")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }

        [HttpPost("{id}/like")]
        public async Task<ActionResult<Unit>> Like(Guid id)
        {
            return await Mediator.Send(new LikeComment.Command { Id = id });
        }

        [HttpDelete("{id}/like")]
        public async Task<ActionResult<Unit>> Unlike(Guid id)
        {
            return await Mediator.Send(new UnlikeComment.Command { Id = id });
        }
    }
}