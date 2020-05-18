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

namespace Application.User
{
    public class ForgetPassword
    {
        public class Query : IRequest<Unit>
        {
            public string Email { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, Unit>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IUrlHelperFactory _urlHelperFactory;
            private readonly IActionContextAccessor _actionContextAccessor;
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IEmailService _emailService;
            public Handler(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IHttpContextAccessor contextAccessor,
            IEmailService emailService)
            {
                _contextAccessor = contextAccessor;
                _userManager = userManager;
                _signInManager = signInManager;
                _urlHelperFactory = urlHelperFactory;
                _actionContextAccessor = actionContextAccessor;
                _emailService = emailService;
            }

            public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
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
                    SenderAddress = "dagardeepak88@gmail.com",
                    ReceiverAddress = "selfishdeepak@gmail.com",
                    Subject = "Forget Password.",
                    TextBody = passwordResetLink,
                };
                await _emailService.SendEmail(email);
                return Unit.Value;
            }
        }
    }
}