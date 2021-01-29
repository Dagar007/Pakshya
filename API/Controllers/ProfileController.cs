using System.Threading.Tasks;
using Application.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfileController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> Get(string id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> Update(Edit.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}