using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Followers;
using Application.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/profile/")]
    public class FollowersController : BaseController
    {
        [HttpPost("{email}/follow")]
        public async Task<ActionResult<Unit>> Follow(string email)
        {
             return await Mediator.Send(new Add.Command{Email = email});
        }
        [HttpDelete("{email}/follow")]
        public async Task<ActionResult<Unit>> Unfollow(string email)
        {
             return await Mediator.Send(new Delete.Command{Email = email});
        }
        [HttpGet("{email}/follow")]
        public async Task<ActionResult<List<Profile>>> GetFollowings(string email, string predicate)
        {
            return await Mediator.Send(new List.Query{Email = email, Predicate = predicate});
        }
    }
}