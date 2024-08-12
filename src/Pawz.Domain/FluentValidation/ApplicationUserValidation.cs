using FluentValidation;
using Pawz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawz.Domain.FluentValidation;
public class ApplicationUserValidation : AbstractValidator<ApplicationUser>
{
    public ApplicationUserValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.").Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .Length(5, 100).WithMessage("Address must be between 5 and 100 characters.");

        RuleFor(x => x.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Creation date cannot be in the future.");

      ///  RuleFor(x => x.Pets)
      ///      .NotNull().WithMessage("Pets collection cannot be null.")
      ///      .Must(pets => pets.Count > 0).WithMessage("At least one pet must be added.");
    }
}
