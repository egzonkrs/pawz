using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

public class UserAccessor : IUserAccessor
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Retrieves the unique identifier of the currently authenticated user.
    /// </summary>
    /// <returns>The user ID as a string, or null if not authenticated.</returns>
    public string GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    /// <summary>
    /// Retrieves the email address of the currently authenticated user.
    /// </summary>
    /// <returns>The user's email address as a string, or null if not authenticated.</returns>
    public string GetEmail()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
    }

    /// <summary>
    /// Retrieves the specified information (e.g., profile picture, address, phone number) for the currently logged-in user.
    /// </summary>
    /// /// <returns>
    /// A string containing the requested user information if available; otherwise, null.
    /// </returns>
    public string GetUserInfo(string infoType)
    {
        var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;

        return infoType.ToLower() switch
        {
            "profilepicture" => user?.ImageUrl,
            "address" => user?.Address,
            "phone" => user?.PhoneNumber,
            _ => null
        };
    }

    /// <summary>
    /// Retrieves the username of the currently authenticated user.
    /// </summary>
    /// <returns>The user's username as a string, or null if not authenticated.</returns>
    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
    }

    /// <summary>
    /// Retrieves the first name of the currently authenticated user.
    /// </summary>
    /// <returns>The user's firstName as a string, or null if not authenticated.</returns>
    public string GetUserFirstName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.GivenName);
    }

    /// <summary>
    /// Retrieves the last name of the currently authenticated user.
    /// </summary>
    /// <returns>The user's LastName as a string, or null if not authenticated.</returns>
    public string GetUserLastName()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Surname);
    }

    /// <summary>
    /// Retrieves the role of the currently authenticated user.
    /// </summary>
    /// <returns>The user's role as a string, or null if not authenticated.</returns>
    public string GetUserRole()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
    }

    /// <summary>
    /// Determines if the current user is authenticated.
    /// </summary>
    /// <returns>True if the user is authenticated; otherwise, false.</returns>
    public bool IsUserAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    public async Task<string> GetCurrentUserIdAsync()
    {
        var user = _httpContextAccessor.HttpContext.User;
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            throw new InvalidOperationException("User is not authenticated or userId claim is missing.");
        }
        return userId;
    }
}
