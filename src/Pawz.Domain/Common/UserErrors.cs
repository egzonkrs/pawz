namespace Pawz.Domain.Common;

/// <summary>
/// Provides centralized error definitions related to user operations.
/// </summary>
public class UserErrors
{
    /// <summary>
    /// Returns an error indicating that a user with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the user that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the user was not found.</returns>
    public static Error NotFound(string id) => new Error("Users.NotFound", $"User with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that a user with the specified email already exists.
    /// </summary>
    /// <param name="email">The email of the user that already exists.</param>
    /// <returns>An <see cref="Error"/> indicating that the email is already in use.</returns>
    public static Error EmailExists(string email) => new Error("Users.EmailExists", $"Email {email} is already in use.");

    /// <summary>
    /// Returns an error indicating that the user creation failed due to a database issue.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that user creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("Users.CreationFailed", "User creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the user creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the user creation process.</returns>
    public static Error CreationUnexpectedError => new Error("Users.CreationUnexpectedError", "An unexpected error occurred during user creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the user update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the user update process.</returns>
    public static Error UpdateUnexpectedError => new Error("Users.UpdateUnexpectedError", "An unexpected error occurred during the user update process.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the user deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the user deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("Users.DeletionUnexpectedError", "An unexpected error occurred during the user deletion process.");

    /// <summary>
    /// Returns an error indicating that the user's login attempt failed.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the login attempt failed.</returns>
    public static Error LoginFailed => new Error("Users.LoginFailed", "Login attempt failed. Please check your credentials and try again.");

    public static Error ClaimFailed => new Error("ClaimFailed", "Failed to assign claims to the user.");
    public static Error UnexpectedError => new Error("UnexpectedError", "An unexpected error occurred.");




}
