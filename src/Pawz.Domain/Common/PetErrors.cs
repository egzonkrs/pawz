namespace Pawz.Domain.Common;

/// <summary>
/// Provides centralized error definitions related to pet operations.
/// </summary>
public static class PetErrors
{
    /// <summary>
    /// Returns an error indicating that a pet with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the pet that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the pet was not found.</returns>
    public static Error NotFound(int id) => new Error("Pets.NotFound", $"Pet with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that no changes were detected during the operation.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no changes were detected.</returns>
    public static Error NoChangesDetected => new Error("Pets.NoChanges", "No changes were detected during the operation.");

    /// <summary>
    /// Returns an error indicating that the pet could not be created due to a database failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the pet creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("Pets.CreationFailed", "Pet creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the pet creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the pet creation process.</returns>
    public static Error CreationUnexpectedError => new Error("Pets.CreationUnexpectedError", "An unexpected error occurred during pet creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the retrieval of pets.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the retrieval of pets.</returns>
    public static Error RetrievalError => new Error("Pets.RetrievalError", "An error occurred while retrieving the list of pets.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the pet update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the pet update process.</returns>
    public static Error UpdateUnexpectedError => new Error("Pets.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the pet deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the pet deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("Pets.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");

    /// <summary>
    /// Returns an error indicating that no pets were found for the specified user.
    /// </summary>
    /// <param name="userId">The ID of the user for whom no pets were found.</param>
    /// <returns>An <see cref="Error"/> indicating that no pets were found for the user.</returns>
    public static Error NoPetsFoundForUser(string userId) => new Error("Pets.NoPetsFoundForUser", $"No pets were found for user with ID {userId}.");
}
