using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Web.Extensions;
using Pawz.Web.Models;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;
public class UsersController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly IValidator<RegisterViewModel> _validator;

    public UsersController(IIdentityService identityService, IValidator<RegisterViewModel> validator)
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
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        var validationResult = await _validator.ValidateAsync(registerViewModel);

        if (validationResult.IsValid is false)
        {
            validationResult.AddToModelState(ModelState);
            return View(registerViewModel);
        }

        var registerRequest = new RegisterRequest
        {
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            Email = registerViewModel.Email,
            Password = registerViewModel.Password
        };

        var registerResult = await _identityService.RegisterAsync(registerRequest);

        if (registerResult.IsSuccess is false)
        {
            registerResult.AddToModelState(ModelState);
            return View(registerViewModel);
        }

        return RedirectToAction("Login", "Users");
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
