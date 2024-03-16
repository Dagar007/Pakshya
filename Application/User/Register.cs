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
using Microsoft.Extensions.Configuration;
using Persistence;

namespace Application.User
{
    public class Register
    {
        public class RegisterCommand : IRequest<Unit>
        {
            public string DisplayName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public DateTime Birthday { get; set; }
            public string Gender { get; set; }

        }

        public class CommandValidator : AbstractValidator<RegisterCommand>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
                RuleFor(x => x.Birthday.Year).LessThan(DateTime.UtcNow.Year - 17);
                RuleFor(x => x.Gender).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<RegisterCommand, Unit>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IUrlHelperFactory _urlHelperFactory;
            private readonly IActionContextAccessor _actionContextAccessor;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IConfiguration _configuration;
            private readonly IEmailService _emailService;
            private readonly DataContext _context;
            public Handler(UserManager<AppUser> userManager,
                IUrlHelperFactory urlHelperFactory,
                IActionContextAccessor actionContextAccessor,
                IHttpContextAccessor contextAccessor,
                IConfiguration configuration,
                IEmailService emailService,
                DataContext context)
            {
                _contextAccessor = contextAccessor;
                _configuration = configuration;
                _userManager = userManager;
                _urlHelperFactory = urlHelperFactory;
                _actionContextAccessor = actionContextAccessor;
                _emailService = emailService;
                _context = context;
            }

            public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                if (await _context.Users.Where(x => x.Email.ToLower() == request.Email.ToLower()).AnyAsync(cancellationToken: cancellationToken))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });
                }
                var user = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.Email,
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
                    new { user = user.Id, token }, _contextAccessor.HttpContext.Request.Scheme);
                    var email = new EmailDto
                    {
                        SenderAddress = _configuration["SenderAddress"],
                        ReceiverAddress = "dagardeepak88@gmail.com",
                        Subject = "Confirm Email.",
                        TextBody = passwordResetLink,
                    };
                    await _emailService.SendEmail(email);
                    return Unit.Value;
                }
                
                var errors = result.Errors.Aggregate("", (current, error) => current + error.Description);
                throw new Exception(errors);
            }

        }
    }
}