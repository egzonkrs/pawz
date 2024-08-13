using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Web.Models;
using Pawz.Web.Validators;
using System.Threading.Tasks;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System;
using FluentValidation.AspNetCore;

namespace Pawz.Web.Controllers
{
public class UsersController : Controller
{
    private readonly IIdentityService _identityService;
    private IValidator<RegisterViewModel> _validator;
    public UsersController(IIdentityService identityService, IValidator<RegisterViewModel> validator)
    {
        _identityService = identityService;
        _validator = validator;
    }

        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Home/Register.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _validator.ValidateAsync(model);

            // Debug output
            Console.WriteLine("Validation Result:");
            Console.WriteLine($"Is Valid: {result.IsValid}");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Property: {error.PropertyName}, Error: {error.ErrorMessage}");
            }

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    result.AddToModelState(this.ModelState);
                }
                return View(model);
            }

            var user = new RegisterRequest
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var registerResult = await _identityService.RegisterAsync(user);

            if (registerResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            // Handle registration failure (e.g., add errors to ModelState)
            ModelState.AddModelError("", "Registration failed. Please try again.");
            return View(model);
        }


        [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var loginRequest = new LoginRequest
        {
            Email = model.Email,
            Password = model.Password
        };

        var result = await _identityService.LoginAsync(loginRequest);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(model);
    }
}
}
