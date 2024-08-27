namespace Pawz.Domain.Common;

/// <summary>
/// Provides centralized error definitions related to location operations.
/// </summary>
public static class LocationErrors
{
    /// <summary>
    /// Returns an error indicating that a location with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the location that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the location was not found.</returns>
    public static Error NotFound(int id) => new Error("Locations.NotFound", $"Location with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that no changes were detected during the operation.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no changes were detected.</returns>
    public static Error NoChangesDetected => new Error("Locations.NoChanges", "No changes were detected during the operation.");

    /// <summary>
    /// Returns an error indicating that the location could not be created due to a database failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the location creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("Locations.CreationFailed", "Location creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the location creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the location creation process.</returns>
    public static Error CreationUnexpectedError => new Error("Locations.CreationUnexpectedError", "An unexpected error occurred during location creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the retrieval of locations.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the retrieval of locations.</returns>
    public static Error RetrievalError => new Error("Locations.RetrievalError", "An error occurred while retrieving the list of locations.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the location update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the location update process.</returns>
    public static Error UpdateUnexpectedError => new Error("Locations.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the location deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the location deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("Locations.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");
}
