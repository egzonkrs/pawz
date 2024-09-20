using FluentValidation;
using Pawz.Web.Models.Pet;

namespace Pawz.Web.Validators;

public class AdoptionRequestModelValidator : AbstractValidator<AdoptionRequestCreateModel>
{
    public AdoptionRequestModelValidator()
    {
        RuleFor(x => x.CityId)
           .NotNull()
           .NotEmpty().WithMessage("City is required.")
           .GreaterThan(0).WithMessage("City must be selected.");

        RuleFor(x => x.CountryId)
            .NotNull()
            .NotEmpty().WithMessage("Country is required.")
            .GreaterThan(0).WithMessage("Country must be selected.");

        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(200).WithMessage("Address must be 200 characters or fewer.");

        RuleFor(x => x.PostalCode)
            .NotNull()
            .NotEmpty().WithMessage("Postal code is required.")
            .MaximumLength(10).WithMessage("Postal code must be 10 characters or fewer.");

        RuleFor(x => x.ContactNumber)
            .NotNull()
            .NotEmpty().WithMessage("Contact number is required.")
            .Matches(@"^\+?\d{1,3}\s?\d{2}\s?\d{3}\s?\d{3}$").WithMessage("Contact number must be in the format +XXX XX XXX XXX.");

        RuleFor(x => x.IsRentedProperty)
            .NotNull().WithMessage("Please specify whether you live in a rented property.");

        RuleFor(x => x.HasOutdoorSpace)
            .NotNull().WithMessage("Please specify whether you have outdoor space.");

        RuleFor(x => x.OwnsOtherPets)
            .NotNull().WithMessage("Please specify whether you own other pets.");

        RuleFor(x => x.OutdoorSpaceDetails)
            .MaximumLength(500).WithMessage("Outdoor space details must be 500 characters or fewer.")
            .When(x => !string.IsNullOrEmpty(x.OutdoorSpaceDetails));

        RuleFor(x => x.OtherPetsDetails)
            .MaximumLength(500).WithMessage("Other pets details must be 500 characters or fewer.")
            .When(x => !string.IsNullOrEmpty(x.OtherPetsDetails));

        RuleFor(x => x.Message)
            .MaximumLength(1000).WithMessage("Message must be 1000 characters or fewer.")
            .When(x => !string.IsNullOrEmpty(x.Message));
    }
}
