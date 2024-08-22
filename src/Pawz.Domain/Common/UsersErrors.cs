namespace Pawz.Domain.Common;

/// <summary>
/// Provides centralized error definitions related to user operations.
/// </summary>
public static class UsersErrors
{
    /// <summary>
    /// Returns an error indicating that a user with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the user that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the user with the specified ID was not found.</returns>
    public static Error NotFound(int id) => new Error("Users.NotFound", $"User with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that no changes were detected during the operation.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no changes were detected during the operation.</returns>
    public static Error NoChangesDetected => new Error("Users.NoChanges", "No changes were detected during the operation.");

    /// <summary>
    /// Returns an error indicating that the user could not be created due to a database failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the user creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("Users.CreationFailed", "User creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the user creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during user creation.</returns>
    public static Error CreationUnexpectedError => new Error("Users.CreationUnexpectedError", "An unexpected error occurred during user creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the retrieval of users.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the retrieval of users.</returns>
    public static Error RetrievalError => new Error("Users.RetrievalError", "An error occurred while retrieving the list of users.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the user update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the update process.</returns>
    public static Error UpdateUnexpectedError => new Error("Users.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the user deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("Users.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");

    /// <summary>
    /// Returns a generic error indicating that the email or password provided is incorrect.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the email or password is incorrect.</returns>
    public static Error IncorrectEmailOrPassword => new Error("Users.Error", "Incorrect email or password.");
}
