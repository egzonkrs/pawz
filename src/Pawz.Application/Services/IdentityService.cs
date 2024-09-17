using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Application.Models.Identity;
using Pawz.Application.Models.UserModel;
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

    /// <summary>
    /// Handles user login operations.
    /// </summary>
    /// <param name="request">The login request containing email and password.</param>
    /// <returns>A result indicating success or failure of the login operation.</returns>
    public async Task<Result<bool>> LoginAsync(LoginRequest request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogError("Login failed for Email: {Email} - User not found.", request.Email);
                return Result<bool>.Failure(UsersErrors.IncorrectEmailOrPassword);
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("Login succeeded for User with Email: {Email} ", request.Email);
                return Result<bool>.Success();
            }

            _logger.LogError("Login failed for Email: {Email} - Incorrect password.", request.Email);
            return Result<bool>.Failure(UsersErrors.IncorrectEmailOrPassword);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {ServiceName} while attempting to log in User with Email: {Email}",
                             nameof(IdentityService), request.Email);
            return Result<bool>.Failure(UsersErrors.UnexpectedError);
        }
    }

    /// <summary>
    /// Handles user registration operations.
    /// </summary>
    /// <param name="request">The registration request containing user details and password.</param>
    /// <returns>A result indicating success or failure of the registration operation.</returns>
    public async Task<Result<bool>> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var userExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email);
            if (userExists)
            {
                _logger.LogError("Registration failed for Email: {Email} - User already exists.", request.Email);
                return Result<bool>.Failure(UsersErrors.UserAlreadyExists(request.Email));
            }

            var user = new ApplicationUser
            {
                UserName = Guid.NewGuid().ToString(),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.UtcNow,
            };

            _logger.LogInformation("Started registering User with Email: {Email} and CreatedAt: {CreatedAt}", request.Email, user.CreatedAt);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded is false)
            {
                return Result<bool>.Failure(UsersErrors.CreationFailed);
            }

            var userClaims = new List<Claim>
                {
                     new Claim(ClaimTypes.NameIdentifier, user.Id),
                     new Claim(ClaimTypes.Email, user.Email),
                     new Claim(ClaimTypes.GivenName, user.FirstName),
                     new Claim(ClaimTypes.Surname, user.LastName),
                     new Claim(ClaimTypes.Role, "User")
                };

            var claimResult = await _userManager.AddClaimsAsync(user, userClaims);

            if (claimResult.Succeeded is false)
            {
                _logger.LogError("User creation failed for Email: {Email} with Errors: {Errors}",
                                 request.Email, string.Join(",", result.Errors.Select(x => x.Description)));

                return Result<bool>.Failure(UsersErrors.ClaimFailed);
            }

            _logger.LogInformation("User registered successfully with Email: {Email}", request.Email);
            return Result<bool>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {ServiceName} while attempting to register User with Email: {Email}",
                             nameof(IdentityService), request.Email);
            return Result<bool>.Failure(UsersErrors.UnexpectedError);
        }
    }

    /// <summary>
    /// Logs out the currently authenticated user.
    /// </summary>
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

    /// <summary>
    /// Handles user profile update operations.
    /// </summary>
    /// <param name="request">The update request containing user details to be updated.</param>
    /// <returns>A result indicating success or failure of the update operation.</returns>
    public async Task<Result<bool>> EditUserAsync(EditUserRequest request)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogError("Edit failed for UserId: {UserId} - User not found.", request.UserId);
                return Result<bool>.Failure(UsersErrors.NotFound(request.UserId));
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Address = request.Address;
            user.PhoneNumber = request.PhoneNumber;

            _logger.LogInformation("Started editing User with UserId: {UserId}", request.UserId);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError("User edit failed for UserId: {UserId} with Errors: {Errors}",
                                 request.UserId, string.Join(",", result.Errors.Select(x => x.Description)));

                return Result<bool>.Failure(UsersErrors.UpdateUnexpectedError);
            }

            if (result.Succeeded && result.Errors.Count() == 0)
            {
                _logger.LogInformation("No changes detected for UserId: {UserId}.", request.UserId);
                return Result<bool>.Failure(UsersErrors.NoChangesDetected);
            }

            _logger.LogInformation("User edited successfully with UserId: {UserId}", request.UserId);
            return Result<bool>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {ServiceName} while attempting to edit User with UserId: {UserId}",
                             nameof(IdentityService), request.UserId);
            return Result<bool>.Failure(UsersErrors.UpdateUnexpectedError);
        }
    }

    public async Task<ApplicationUser> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException($"User with ID {userId} not found.");
        }
        return user;
    }
}
