using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Pawz.Web.Validators
{
    public class MinCollectionCountValidator : ValidationAttribute
    {
        private readonly int _minCount;

        public MinCollectionCountValidator(int minCount)
        {
            _minCount = minCount;
            ErrorMessage = $"The collection must contain at least {_minCount} item(s).";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is ICollection collection && collection.Count < _minCount)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}