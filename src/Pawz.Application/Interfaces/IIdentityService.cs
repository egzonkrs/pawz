using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface IIdentityService
{
    /// <summary>
    /// Registers a new user with the provided details.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RegisterAsync();

    /// <summary>
    /// Logs in a user with the provided credentials.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task LoginAsync();
}

