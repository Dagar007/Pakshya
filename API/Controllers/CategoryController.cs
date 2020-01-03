using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Categories;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/posts")]
    public class CategoryController : BaseController
    {
        [HttpGet("category")]
        public async Task<List<Category>> List ()
        {
           return await Mediator.Send(new List.Query());
        }
    }
}