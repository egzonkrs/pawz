using Microsoft.AspNetCore.Identity;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result<bool>> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<bool>.Failure(UsersErrors.IncorrectEmailOrPassword);
        }

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return Result<bool>.Success();
        }

        return Result<bool>.Failure(UsersErrors.IncorrectEmailOrPassword);
    }

    public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        return result;
    }
}
