using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using Pawz.Web.Models;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;
public class UsersController : Controller
{
    private readonly IIdentityService _identityService;

    public UsersController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            AddModelErrorIfEmpty(model.Email, nameof(model.Email), "Email is required.");
            AddModelErrorIfEmpty(model.Password, nameof(model.Password), "Password is required.");
            AddModelErrorIfEmpty(model.FirstName, nameof(model.FirstName), "First Name is required.");
            AddModelErrorIfEmpty(model.LastName, nameof(model.LastName), "Last Name is required.");

            return View(model);
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var result = await _identityService.RegisterAsync(user, model.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return View(model);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }

    /// <summary>
    /// Helper method to add model error if a field is empty or null
    /// </summary>
    /// <param name="fieldValue"></param>
    /// <param name="fieldName"></param>
    /// <param name="errorMessage"></param>
    private void AddModelErrorIfEmpty(string fieldValue, string fieldName, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(fieldValue))
        {
            ModelState.AddModelError(fieldName, errorMessage);
        }
    }
}
