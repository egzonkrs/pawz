using FluentValidation;
using Pawz.Web.Models;
using Pawz.Web.Models.Identity;

namespace Pawz.Web.Validators;

public class LoginModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginModelValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty().WithMessage("Password is required.");
    }
}
