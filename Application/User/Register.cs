using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User
{
    public class Register
    {
        public class Command : IRequest<Unit>
        {
            public string DisplayName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public DateTime Birthday { get; set; }
            public string Gender { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
                RuleFor(x => x.Birthday.Year).LessThan(DateTime.Now.Year - 17);
                RuleFor(x => x.Gender).NotEmpty();

            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
             private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IUrlHelperFactory _urlHelperFactory;
            private readonly IActionContextAccessor _actionContextAccessor;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IEmailService _emailService;
            private readonly DataContext _context;
            public Handler(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IHttpContextAccessor contextAccessor,
            IEmailService emailService,
            DataContext context)
            {
                _contextAccessor = contextAccessor;
                _userManager = userManager;
                _signInManager = signInManager;
                _urlHelperFactory = urlHelperFactory;
                _actionContextAccessor = actionContextAccessor;
                _emailService = emailService;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });
                }
                if (await _context.Users.Where(x => x.UserName == request.Username).AnyAsync())
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });
                }

                var user = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.Username,
                    Gender = request.Gender,
                    Birthday = request.Birthday,
                    IsActive = true,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

                     var passwordResetLink = urlHelper.Action("ConfirmEmail", "ConfirmEmail",
                    new { user = user.Id, token = token }, _contextAccessor.HttpContext.Request.Scheme);
                    var email = new EmailDto
                    {
                        SenderAddress = "dagardeepak88@gmail.com",
                        ReceiverAddress = "selfishdeepak@gmail.com",
                        Subject = "Confirm Email.",
                        TextBody = passwordResetLink,
                    };
                    await _emailService.SendEmail(email);
                    return Unit.Value;
                    // return new User
                    // {
                    //     DisplayName = user.DisplayName,
                    //     Token = _generator.CreateToken(user),
                    //     Username = user.UserName,
                    //     Image = user.Photos?.FirstOrDefault(x => x.IsMain)?.Url
                    // };
                }

                throw new Exception("problem creating user.");
            }

        }
    }
}