using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

public sealed class IdentityService : IIdentityService
{
    private readonly ILogger<IIdentityService> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<IIdentityService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task<Result<bool>> LoginAsync(LoginRequest request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogError("Login failed for Email: {Email} - User not found.",request.Email);
                return Result<bool>.Failure(UsersErrors.IncorrectEmailOrPassword);
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("Login succeeded for User with Email: {Email} ",request.Email);
                return Result<bool>.Success();
            }

            _logger.LogError("Login failed for Email: {Email} - Incorrect password.",request.Email);
            return Result<bool>.Failure(UsersErrors.IncorrectEmailOrPassword);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {ServiceName} while attempting to log in User with Email: {Email}",
                             nameof(IdentityService),request.Email);
            return Result<bool>.Failure(UserErrors.UnexpectedError);
        }
    }

    public async Task<Result<bool>> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var user = new ApplicationUser
            {
                UserName = request.FirstName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.UtcNow,
            };

            _logger.LogInformation("Started registering User with Email: {Email} and CreatedAt: {CreatedAt}" , request.Email,user.CreatedAt);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded is false)
            {
                return Result<bool>.Failure(UserErrors.CreationFailed);
            }

            var userClaims = new List<Claim>
                {
                     new Claim(ClaimTypes.NameIdentifier, user.Id),
                     new Claim(ClaimTypes.Email, user.Email),
                     new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(ClaimTypes.Role, "User")
                };

            var claimResult = await _userManager.AddClaimsAsync(user, userClaims);

            if (claimResult.Succeeded is false)
            {
                _logger.LogError("User creation failed for Email: {Email} with Errors: {Errors}",
                                 request.Email, string.Join(",", result.Errors.Select(x => x.Description)));

                return Result<bool>.Failure(UserErrors.ClaimFailed);
            }

            _logger.LogInformation("User registered successfully with Email: {Email}", request.Email);
            return Result<bool>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {ServiceName} while attempting to register User with Email: {Email}",
                             nameof(IdentityService), request.Email);
            return Result<bool>.Failure(UserErrors.UnexpectedError);
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while logging out the user.");
        }
    }
}
