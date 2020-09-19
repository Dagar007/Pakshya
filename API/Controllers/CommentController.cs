using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Comments;
using Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Comments.Create;

namespace API.Controllers
{
    public class CommentController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<CommentDto>> Create(CommentCreateCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpGet("{postId}")]
        public async Task<ActionResult<List<CommentDto>>> List(Guid postId, [FromQuery]Params  commentParams)
        {
            return await Mediator.Send(new List.Query(postId, commentParams));
        }
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