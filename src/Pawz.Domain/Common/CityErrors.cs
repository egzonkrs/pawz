namespace Pawz.Domain.Common;

/// <summary>
/// Provides centralized error definitions related to city operations.
/// </summary>
public static class CityErrors
{
    /// <summary>
    /// Returns an error indicating that a city with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the city that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the city was not found.</returns>
    public static Error NotFound(int id) => new Error("Cities.NotFound", $"City with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that no changes were detected during the operation.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no changes were detected during the operation.</returns>
    public static Error NoChangesDetected => new Error("Cities.NoChanges", "No changes were detected during the operation.");

    /// <summary>
    /// Returns an error indicating that the city could not be created due to a database failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the city creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("Cities.CreationFailed", "City creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the city creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the city creation process.</returns>
    public static Error CreationUnexpectedError => new Error("Cities.CreationUnexpectedError", "An unexpected error occurred during city creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the retrieval of cities.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the retrieval of cities.</returns>
    public static Error RetrievalError => new Error("Cities.RetrievalError", "An error occurred while retrieving the list of cities.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the city update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the city update process.</returns>
    public static Error UpdateUnexpectedError => new Error("Cities.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the city deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the city deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("Cities.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");
}
