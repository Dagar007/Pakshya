using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Posts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class PostsController : BaseController
  {
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<PostConcise>>> List([FromQuery]Params  postParams)
    {
      return await Mediator.Send(new List.Query(postParams));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PostConcise>> Details(Guid id)
    {
      return await Mediator.Send(new Details.Query { Id = id });
    }
    [HttpPost]
    public async Task<ActionResult<Unit>> Create([FromForm] IFormFile File, [FromForm] string jsonPost)
    {
      return await Mediator.Send(new Create.Command {File = File, JsonPost = jsonPost});
    }

    [HttpPost("{id}")]
    [Authorize(Policy = "IsPostHost")]
    public async Task<ActionResult<Unit>> Edit (Guid id, [FromForm] IFormFile File, [FromForm] string jsonPost)
    {
      return await Mediator.Send(new Edit.Command{Id = id, File = File, JsonPost = jsonPost});
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "IsPostHost")]
    public async Task<ActionResult<Unit>> Delete(Guid id)
    {
      return await Mediator.Send(new Delete.Command{Id = id} );
    }

    [HttpPost("{id}/like")]
    public async Task<ActionResult<Unit>> Like(Guid id)
    {
      return await Mediator.Send(new LikePost.Command{Id = id});
    }

    [HttpDelete("{id}/like")]
     public async Task<ActionResult<Unit>> Unlike(Guid id)
    {
      return await Mediator.Send(new UnLike.Command{Id = id});
    }
    [AllowAnonymous]
    [HttpGet("stats")]
    public async Task<ActionResult<List<MostDisscussedPostDto>>> Stats()
    {
      return await Mediator.Send(new MostDiscussedPost.Query() );
    }

  }
}