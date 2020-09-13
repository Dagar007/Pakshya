using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;

namespace Application.User
{
    public class ForgetPassword
    {
        public class ForgetPasswordQuery : IRequest<Unit>
        {
            public string Email { get; set; }
        }

        public class QueryValidator : AbstractValidator<ForgetPasswordQuery>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<ForgetPasswordQuery, Unit>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IUrlHelperFactory _urlHelperFactory;
            private readonly IActionContextAccessor _actionContextAccessor;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IConfiguration _configuration;
            private readonly IEmailService _emailService;
            public Handler(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IHttpContextAccessor contextAccessor,
            IConfiguration configuration,
            IEmailService emailService)
            {
                _contextAccessor = contextAccessor;
                _configuration = configuration;
                _userManager = userManager;
                _signInManager = signInManager;
                _urlHelperFactory = urlHelperFactory;
                _actionContextAccessor = actionContextAccessor;
                _emailService = emailService;
            }

            public async Task<Unit> Handle(ForgetPasswordQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    throw new RestException(HttpStatusCode.BadRequest);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                 var urlHelper =
                    _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
                var passwordResetLink = urlHelper.Action("ResetPassword", "ForgetPassword",
                    new { user = user.Id, token = token }, _contextAccessor.HttpContext.Request.Scheme);
                var email = new EmailDto
                {
                    SenderAddress = _configuration["SenderAddress"],
                    ReceiverAddress = "dagardeepak88@gmail.com",
                    Subject = "Forget Password.",
                    TextBody = passwordResetLink,
                };
                await _emailService.SendEmail(email);
                return Unit.Value;
            }
        }
    }
}