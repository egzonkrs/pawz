using FluentValidation;
using Pawz.Web.Models.Pet;

namespace Pawz.Web.Validators;

public class AdoptionRequestModelValidator : AbstractValidator<AdoptionRequestCreateModel>
{
    public AdoptionRequestModelValidator()
    {
        RuleFor(x => x.CityId)
            .NotNull()
            .GreaterThan(0).WithMessage("City must be selected.");

        RuleFor(x => x.CountryId)
            .NotNull()
            .GreaterThan(0).WithMessage("Country must be selected.");

        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(200).WithMessage("Address must be 200 characters or fewer.");

        RuleFor(x => x.PostalCode)
            .NotNull()
            .NotEmpty().WithMessage("Postal code is required.")
            .MaximumLength(10).WithMessage("Postal code must be 10 characters or fewer.");
    }
}
