using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InterestController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<UserInterestDto>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Add(AddUserInterestsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}/user")]
        public async Task<ActionResult<List<Guid>>> UserIntrests(string id)
        {
            return await Mediator.Send(new ListUserInterests.UserInterestsQuery{Id= id});
        }

    }
}