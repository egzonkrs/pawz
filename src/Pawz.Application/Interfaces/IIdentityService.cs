using Pawz.Application.Models.Identity;
using Pawz.Application.Models.UserModel;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IIdentityService
{
    /// <summary>
    /// Registers a new user with the provided details.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result<bool>> RegisterAsync(RegisterRequest request);

    /// <summary>
    /// Logs in a user with the provided credentials.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result<bool>> LoginAsync(LoginRequest request);

    /// <summary>
    /// Logs out the currently authenticated user.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task LogoutAsync();

    /// <summary>
    /// Handles user profile update operations.
    /// </summary>
    /// <param name="request">The update request containing user details to be updated.</param>
    /// <returns>A result indicating success or failure of the update operation.</returns>
    Task<Result<bool>> EditUserAsync(EditUserRequest request);

    Task<ApplicationUser> GetUserByIdAsync(string userId);
}

