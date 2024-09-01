using FluentValidation;
using Pawz.Web.Models;

namespace Pawz.Web.Validators;

public class PetCreateViewModelValidator : AbstractValidator<PetCreateViewModel>
{
    public PetCreateViewModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be 50 characters or fewer.");

        RuleFor(x => x.BreedId)
            .NotNull()
            .NotEmpty().WithMessage("Breed is required.")
            .GreaterThan(0).WithMessage("Breed must be selected.");

        RuleFor(x => x.AgeYears)
            .NotNull().WithMessage("Age in years is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Age in years must be 0 or greater.")
            .LessThanOrEqualTo(20).WithMessage("Age in years must be 20 or fewer.");

        RuleFor(x => x.AgeMonths)
            .NotNull().WithMessage("Age in months is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Age in months must be 0 or greater.")
            .LessThan(12).WithMessage("Age in months must be less than 12.");

        RuleFor(x => x.About)
            .NotNull()
            .NotEmpty().WithMessage("About section is required.")
            .MaximumLength(500).WithMessage("About section must be 500 characters or fewer.");

        RuleFor(x => x.Price)
            .NotNull().WithMessage("Price is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Price must be a positive value.");

        //RuleFor(x => x.LocationId)
        //    .NotNull()
        //    .NotEmpty().WithMessage("Location is required.")
        //    .GreaterThan(0).WithMessage("Location must be selected.");

        RuleFor(x => x.Status)
            .NotNull().WithMessage("Status is required.")
            .IsInEnum().WithMessage("A valid pet status is required.");
    }
}
