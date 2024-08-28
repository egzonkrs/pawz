namespace Pawz.Domain.Common;

/// <summary>
/// Provides centralized error definitions related to country operations.
/// </summary>
public static class CountryErrors
{
    /// <summary>
    /// Returns an error indicating that a country with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the country that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the country was not found.</returns>
    public static Error NotFound(int id) => new Error("Countries.NotFound", $"Country with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that no changes were detected during the operation.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no changes were detected during the operation.</returns>
    public static Error NoChangesDetected => new Error("Countries.NoChanges", "No changes were detected during the operation.");

    /// <summary>
    /// Returns an error indicating that the country could not be created due to a database failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the country creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("Countries.CreationFailed", "Country creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the country creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the country creation process.</returns>
    public static Error CreationUnexpectedError => new Error("Countries.CreationUnexpectedError", "An unexpected error occurred during country creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the retrieval of countries.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the retrieval of countries.</returns>
    public static Error RetrievalError => new Error("Countries.RetrievalError", "An error occurred while retrieving the list of countries.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the country update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the country update process.</returns>
    public static Error UpdateUnexpectedError => new Error("Countries.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the country deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the country deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("Countries.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");
}
