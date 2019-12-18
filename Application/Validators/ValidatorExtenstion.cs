using FluentValidation;

namespace Application.Validators
{
    public static class ValidatorExtenstion
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                            .NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters")
                            .Matches("[A-Z]").WithMessage("Password must conatin at least 1 uppercase letter")
                            .Matches("[a-z]").WithMessage("Password must conatin at least 1 lowercase letter")
                            .Matches("[0-9]").WithMessage("Password must conatin at least 1 number")
                            .Matches("[^a-zA-Z0-9]").WithMessage("Password must conatin non alphanumeric");
            return options;
        }
    }

}