using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Web.Extensions;
using Pawz.Web.Models;
using System;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;
public class UsersController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly IValidator<RegisterRequest> _validator;

    public UsersController(IIdentityService identityService, IValidator<RegisterRequest> validator)
    {
        _identityService = identityService;
        _validator = validator;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new RegisterRequest
        {
            Email = model.Email,
            Password = model.Password,
            FirstName = model.FirstName,
            LastName = model.LastName,
        };

        var validationResult = await _validator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View(model);
        }

        try
        {
            var result = await _identityService.RegisterAsync(user);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            return RedirectToAction("Login", "Users");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
            return View(model);
        }
    }

    public IActionResult Login()
    {
        return View();
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
