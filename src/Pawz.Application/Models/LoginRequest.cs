namespace Pawz.Application.Models;

/// <summary>
/// The Login Request initiated by the user of the application.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// The email of the user that is trying to log in.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The password of the user that is trying to log in.
    /// </summary>
    public string Password { get; set; }
}
