using Microsoft.AspNetCore.Identity;
using Pawz.Application.Models;
using Pawz.Domain.Common;
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
}

