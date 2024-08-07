using Microsoft.AspNetCore.Identity;
using Pawz.Domain.Entities;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IIdentityService
{
    /// <summary>
    /// Registers a new user with the provided details.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);

    /// <summary>
    /// Logs in a user with the provided credentials.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<SignInResult> LoginAsync(string email, string password);
}

