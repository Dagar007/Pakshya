using System.Threading.Tasks;
using Application.User;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ForgetPasswordController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

         public ForgetPasswordController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpGet(Name = "ResetPassword")]
        public IActionResult ResetPassword(string user, string token)
        {
            if (token == null && user == null)
            {
                ModelState.AddModelError("", "Invalid reset token");
            }

            return View();
        }

         [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(resetPasswordDto.User);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                    if (result.Succeeded)
                    {
                        return Redirect(_configuration["ClientUrl"] + "login");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(resetPasswordDto);
                }
                ModelState.AddModelError("", "Invalid User");
                return View(resetPasswordDto);
            }

            return View(resetPasswordDto);
        }
    }
}