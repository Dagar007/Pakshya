using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Posts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
  //[AllowAnonymous]
  public class PostsController : BaseController
  {
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<PostConcise>>> List([FromQuery]PostParams  postParams)
    {
      return await Mediator.Send(new List.Query(postParams));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> Details(Guid id)
    {
      return await Mediator.Send(new Details.Query { Id = id });
    }
    [HttpPost]
    public async Task<ActionResult<Unit>> Create(Create.Command command)
    {
      return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "IsPostHost")]
    public async Task<ActionResult<Unit>> Edit (Guid id, Edit.Command command)
    {
      command.Id = id;
      return await Mediator.Send(command);
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