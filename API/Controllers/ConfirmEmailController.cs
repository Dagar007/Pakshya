using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ConfirmEmailController : Controller
    {
         private readonly UserManager<AppUser> _userManager;
        public ConfirmEmailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet(Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string user, string token)
        {
            var userFromDB = await _userManager.FindByIdAsync(user);
            if (userFromDB == null)
                return View("Error");

            var result = await _userManager.ConfirmEmailAsync(userFromDB, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }
    }
}