using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Posts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
  //[AllowAnonymous]
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
      var command1 = JsonConvert.DeserializeObject<Create.Command1>(jsonPost);
      var command = new Create.Command();
      command.Id = command1.Id;
      command.Heading = command1.Heading;
      command.Description = command1.Description;
      command.Date = command1.Date;
      command.Category = command1.Category;
      command.File = File;

      command.File = File;
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