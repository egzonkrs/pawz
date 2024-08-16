namespace Pawz.Domain.Common;

/// <summary>
/// Provides centralized error definitions related to breed operations.
/// </summary>
public static class BreedErrors
{
    /// <summary>
    /// Returns an error indicating that a breed with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the breed that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the breed was not found.</returns>
    public static Error NotFound(int id) => new Error("Breeds.NotFound", $"Breed with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that no changes were detected during the operation.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no changes were detected.</returns>
    public static Error NoChangesDetected => new Error("Breeds.NoChanges", "No changes were detected during the operation.");

    /// <summary>
    /// Returns an error indicating that the breed could not be created due to a database failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the breed creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("Breeds.CreationFailed", "Breed creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the breed creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the breed creation process.</returns>
    public static Error CreationUnexpectedError => new Error("Breeds.CreationUnexpectedError", "An unexpected error occurred during breed creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the retrieval of breeds.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the retrieval of breeds.</returns>
    public static Error RetrievalError => new Error("Breeds.RetrievalError", "An error occurred while retrieving the list of breeds.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the breed update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the breed update process.</returns>
    public static Error UpdateUnexpectedError => new Error("Breeds.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the breed deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the breed deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("Breeds.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");
}
