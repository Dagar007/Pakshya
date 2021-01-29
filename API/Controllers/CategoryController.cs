using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/posts")]
    public class CategoryController : BaseController
    {
        [HttpGet("category")]
        public async Task<List<CategoryDto>> List ()
        {
           return await Mediator.Send(new List.Query());
        }
        [HttpGet("category/stats")]
        public async Task<List<CategoryStatsDto>> Stats ()
        {
           return await Mediator.Send(new Stats.Query());
        }
    }
}