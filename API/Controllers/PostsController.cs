using System.Collections.Generic;
using Application.Helpers;
using Application.Posts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class PostsController : BaseController
  {
      private readonly ILogger<PostsController> _logger;

      public PostsController(ILogger<PostsController> logger)
      {
          _logger = logger;
      }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<PostConcise>>> List([FromQuery]Params postParams)
    {
      return await Mediator.Send(new List.Query(postParams));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PostConcise>> Details(Guid id)
    {
        using var scope = _logger.BeginScope("Loading Post {PostId}", id);
      return await Mediator.Send(new Details.Query { Id = id });
    }
    [HttpPost]
    public async Task<ActionResult<Unit>> Create([FromForm] Create.Command command)
    {
      return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "IsPostHost")]
    public async Task<ActionResult<Unit>> Edit (Guid id, [FromForm] Edit.Command command)
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
    public async Task<ActionResult<List<MostDiscussedPostDto>>> Stats()
    {
      return await Mediator.Send(new MostDiscussedPost.Query() );
    }

  }
}