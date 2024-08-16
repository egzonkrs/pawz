using System.ComponentModel.DataAnnotations;

namespace Pawz.Web.Models;

public class AdoptionRequest
{
    /// <summary>
    /// Gets or sets the first name of the user that is requesting to adopt.
    /// This is required.
    /// </summary>
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user that is requesting to adopt.
    /// This is required.
    /// </summary>
    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user that is requesting to adopt.
    /// This is required and must be a valid email address.
    /// </summary>
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the location of the user that is requesting to adopt.
    /// This is required.
    /// </summary>
    [Required(ErrorMessage = "Location is required")]
    public string Location { get; set; }

}
