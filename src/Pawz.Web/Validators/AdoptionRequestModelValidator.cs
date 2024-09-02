using FluentValidation;
using Pawz.Web.Models.Pet;

namespace Pawz.Web.Validators;

public class AdoptionRequestModelValidator : AbstractValidator<AdoptionRequestCreateModel>
{
    public AdoptionRequestModelValidator()
    {
        RuleFor(x => x.Countries)
            .NotNull()
            .NotEmpty()
            .WithMessage("County is required");

        RuleFor(x => x.Cities)
            .NotNull()
            .NotEmpty()
            .WithMessage("City is required");

        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty()
            .WithMessage("Address is required");
    }

}
