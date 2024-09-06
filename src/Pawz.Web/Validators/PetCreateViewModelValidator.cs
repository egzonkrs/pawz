using FluentValidation;
using Pawz.Web.Models;
using Pawz.Web.Models.Pet;
using System.Linq;

namespace Pawz.Web.Validators;

public class PetCreateViewModelValidator : AbstractValidator<PetCreateViewModel>
{
    public PetCreateViewModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be 50 characters or fewer.")
            .Matches(@"^[^\d][a-zA-Z\s]*$").WithMessage("Name must not start with a number and should not contain numbers.");

        RuleFor(x => x.BreedId)
            .NotNull()
            .NotEmpty().WithMessage("Breed is required.")
            .GreaterThan(0).WithMessage("Breed must be selected.");

        RuleFor(x => x.SpeciesId)
                .NotNull()
                .NotEmpty().WithMessage("Species is required.")
                .GreaterThan(0).WithMessage("Species must be selected.");

        RuleFor(x => x.AgeYears)
            .NotNull()
            .NotEmpty().WithMessage("Age is required.");

        RuleFor(x => x.About)
            .NotNull()
            .NotEmpty().WithMessage("About section is required.")
            .MaximumLength(500).WithMessage("About section must be 500 characters or fewer.");

        RuleFor(x => x.Price)
            .NotNull().WithMessage("Price is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Price must be a positive value.");

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

        RuleFor(x => x.ImageFiles)
              .NotNull().WithMessage("At least one image is required.")
              .Must(images => images != null && images.Any()).WithMessage("You must upload at least one image.")
              .Must(images => images.All(image => image.Length <= 5 * 1024 * 1024))
              .WithMessage("Each image must be 5MB or smaller.");
    }
}
