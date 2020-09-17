using System.Threading.Tasks;
using Application.Profiles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfileController : BaseController
    {
        [HttpGet("{email}")]
        public async Task<ActionResult<Profile>> Get(string email)
        {
            return await Mediator.Send(new Details.Query { Email = email });
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> Update(Edit.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}