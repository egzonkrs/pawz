using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Application.Models.Pet;
using Pawz.Web.Extensions;
using Pawz.Web.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class UsersController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly IValidator<RegisterViewModel> _validator;
    private readonly IValidator<LoginViewModel> _loginModelValidator;
    private readonly IPetService _petService;
    private readonly IMapper _mapper;

    public UsersController(IIdentityService identityService, IValidator<RegisterViewModel> validator, IValidator<LoginViewModel> loginModelValidator, IPetService petService, IMapper mapper)
    {
        _identityService = identityService;
        _validator = validator;
        _loginModelValidator = loginModelValidator;
        _petService = petService;
        _mapper = mapper;
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
            validationResult.AddErrorsToModelState(ModelState);
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
            registerResult.AddErrorsToModelState(ModelState);
            return View(registerViewModel);
        }

        return RedirectToAction("Login", "Users");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var validationResult = await _loginModelValidator.ValidateAsync(loginViewModel);

        if (validationResult.IsValid is false)
        {
            validationResult.AddErrorsToModelState(ModelState);
            return View(loginViewModel);
        }

        var loginRequest = new LoginRequest
        {
            Email = loginViewModel.Email,
            Password = loginViewModel.Password
        };

        var loginResult = await _identityService.LoginAsync(loginRequest);

        if (loginResult.IsSuccess is false)
        {
            loginResult.AddErrorsToModelState(ModelState);
            return View(loginViewModel);
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _identityService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Profile()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> MyPets(CancellationToken cancellationToken)
    {
        var result = await _petService.GetPetsByUserIdAsync(cancellationToken);

        var pets = result.Value;

        var petResponses = _mapper.Map<IEnumerable<UserPetResponse>>(pets);
        return PartialView("MyPets", petResponses);
    }

    [HttpGet]
    public IActionResult AdoptionRequest()
    {
        return PartialView("AdoptionRequest");
    }

    [HttpGet]
    public IActionResult Adoptions()
    {
        return PartialView("Adoptions");
    }

    [HttpGet]
    public IActionResult MyAdoptions()
    {
        return PartialView("MyAdoptions");
    }

}
