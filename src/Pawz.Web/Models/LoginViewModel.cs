using System.ComponentModel.DataAnnotations;

namespace Pawz.Web.Models;

public class LoginViewModel
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// This is required and must be a valid email address.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password for the user.
    /// This is required and should be a secure password.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
}
