using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pawz.Domain.Common;

namespace Pawz.Web.Extensions;

public static class ValidationExtensions
{
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }

    public static void AddErrorsToModelState(this Result<bool> result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError(error.Code, error.Description);
        }
    }
}
