namespace Pawz.Domain.Common;

/// <summary>
/// Provides centralized error definitions related to adoption operations.
/// </summary>
public static class AdoptionErrors
{
    /// <summary>
    /// Returns an error indicating that an adoption with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the adoption that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the adoption was not found.</returns>
    public static Error NotFound(int id) => new Error("Adoptions.NotFound", $"Adoption with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that no changes were detected during the operation.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no changes were detected.</returns>
    public static Error NoChangesDetected => new Error("Adoptions.NoChanges", "No changes were detected during the operation.");

    /// <summary>
    /// Returns an error indicating that the adoption could not be created due to a database failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the adoption creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("Adoptions.CreationFailed", "Adoption creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the adoption creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the adoption creation process.</returns>
    public static Error CreationUnexpectedError => new Error("Adoptions.CreationUnexpectedError", "An unexpected error occurred during adoption creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the retrieval of adoption.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the retrieval of adoptions.</returns>
    public static Error RetrievalError => new Error("Adoptions.RetrievalError", "An error occurred while retrieving the list of adoptions.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the adoption update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the adoption update process.</returns>
    public static Error UpdateUnexpectedError => new Error("Adoptions.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the adoption deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the adoption deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("Adoptions.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");
}
