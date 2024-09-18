using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Application.Models.Identity;
using Pawz.Application.Models.Pet;
using Pawz.Application.Models.UserModel;
using Pawz.Domain.Entities;
using Pawz.Web.Extensions;
using Pawz.Web.Models;
using Pawz.Web.Models.Pet;
using Pawz.Web.Models.User;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
    private readonly IUserAccessor _userAccessor;

    public UsersController(IIdentityService identityService, IValidator<RegisterViewModel> validator, IValidator<LoginViewModel> loginModelValidator, IPetService petService, IMapper mapper, IUserAccessor userAccessor)
    {
        _identityService = identityService;
        _validator = validator;
        _loginModelValidator = loginModelValidator;
        _petService = petService;
        _mapper = mapper;
        _userAccessor = userAccessor;
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

    // public async Task<IActionResult> Profile()
    // {
    //     var userId = await _userAccessor.GetCurrentUserIdAsync();
    //     var user = await _identityService.GetUserByIdAsync(userId);
    //     return View(user);
    // }

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

    public ApplicationUserViewModel GetUserById(string userId)
    {
        // Call the async method and wait for it to complete
        var result = _identityService.GetUserByIdAsync().GetAwaiter().GetResult();

        // Check if the result was successful or if the user was not found
        if (!result.IsSuccess || result.Value == null)
        {
            // Return a default model if the user was not found
            return new ApplicationUserViewModel
            {
                FirstName = "Unknown",
                LastName = "User",
                Address = "Unknown",
                PhoneNumber = "Unknown",
                ImageUrl = ""
            };
        }

        // Map the ApplicationUser to ApplicationUserViewModel and return it
        return new ApplicationUserViewModel
        {
            FirstName = result.Value.FirstName,
            LastName = result.Value.LastName,
            Address = result.Value.Address,
            PhoneNumber = result.Value.PhoneNumber,
            ImageUrl = result.Value.ImageUrl // Ensure this field exists in ApplicationUser
        };
    }


    [HttpGet]
    public IActionResult EditProfileForm(string userId)
    {
        var model = GetUserById(userId);
        return PartialView("EditProfileForm", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(ApplicationUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var editUserRequest = new EditUserRequest
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _identityService.EditUserAsync(editUserRequest);

            if (result.IsSuccess)
            {
                return RedirectToAction("Profile");
            }
            else
            {
                ModelState.AddModelError("", "Error updating the user profile.");
            }
        }
        return View("Profile", model);
    }
}
