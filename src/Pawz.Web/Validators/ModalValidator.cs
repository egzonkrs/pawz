using FluentValidation;
using Pawz.Web.Models;

namespace Pawz.Web.Validators;

public class ModalValidator : AbstractValidator<AdoptionRequestCreateModel>
{
    public ModalValidator()
    {
        RuleFor(x => x.Country)
            .NotNull()
            .NotEmpty()
            .WithMessage("County is required");

        RuleFor(x => x.City)
            .NotNull()
            .NotEmpty()
            .WithMessage("City is required");

        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty()
            .WithMessage("Address is required");
    }

}
