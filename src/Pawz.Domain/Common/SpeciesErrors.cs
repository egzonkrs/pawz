namespace Pawz.Domain.Common;

public static class SpeciesErrors
{
    /// <summary>
    /// Returns an error indicating that a species with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the species that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the species was not found.</returns>
    public static Error NotFound(int id) => new Error("Species.NotFound", $"Species with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that no changes were detected during the operation.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no changes were detected.</returns>
    public static Error NoChangesDetected => new Error("Species.NoChanges", "No changes were detected during the operation.");

    /// <summary>
    /// Returns an error indicating that the species could not be created due to a database failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the species creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("Species.CreationFailed", "Species creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the species creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the species creation process.</returns>
    public static Error CreationUnexpectedError => new Error("Species.CreationUnexpectedError", "An unexpected error occurred during species creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the retrieval of species.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the retrieval of species.</returns>
    public static Error RetrievalError => new Error("Species.RetrievalError", "An error occurred while retrieving the list of species.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the species update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the species update process.</returns>
    public static Error UpdateUnexpectedError => new Error("Species.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the species deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the species deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("Species.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");
}
