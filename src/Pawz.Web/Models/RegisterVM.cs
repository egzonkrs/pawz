using System.ComponentModel.DataAnnotations;

namespace Pawz.Web.Models;

public class RegisterVM
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
    [DataType(DataType.Password, ErrorMessage = "Invalid password format.")]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the confirmation password.
    /// This is required and must match the password.
    /// </summary>
    [Required(ErrorMessage = "Confirm Password is required.")]
    [DataType(DataType.Password, ErrorMessage = "Invalid password format.")]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// This is required.
    /// </summary>
    [Required(ErrorMessage = "First Name is required.")]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// This is required.
    /// </summary>
    [Required(ErrorMessage = "Last Name is required.")]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the address of the user.
    /// This is optional.
    /// </summary>
    public string Address { get; set; }

}
