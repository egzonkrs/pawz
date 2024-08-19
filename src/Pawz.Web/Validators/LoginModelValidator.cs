using FluentValidation;
using Pawz.Web.Models;

namespace Pawz.Web.Validators;

public class LoginModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginModelValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email is required.")
           .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
