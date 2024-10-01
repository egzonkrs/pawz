namespace Pawz.Domain.Common;


/// <summary>
/// Provides centralized error definitions related to adoption request operations.
/// </summary>
public class AdoptionRequestErrors
{
    /// <summary>
    /// Returns an error indicating that an adoption with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the adoption request that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the adoption was not found.</returns>
    public static Error NotFound(int id) => new Error("AdoptionRequests.NotFound", $"Adoption Request with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that no changes were detected during the operation.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no changes were detected.</returns>
    public static Error NoChangesDetected => new Error("AdoptionRequests.NoChanges", "No changes were detected during the operation.");

    /// <summary>
    /// Returns an error indicating that the adoption could not be created due to a database failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the adoption request creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("AdoptionRequests.CreationFailed", "Adoption Request creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the adoption request creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the adoption request creation process.</returns>
    public static Error CreationUnexpectedError => new Error("AdoptionRequests.CreationUnexpectedError", "An unexpected error occurred during adoption request creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the retrieval of adoption.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the retrieval of adoption requests.</returns>
    public static Error RetrievalError => new Error("AdoptionRequests.RetrievalError", "An error occurred while retrieving the list of adoption requests.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the adoption request update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the adoption request update process.</returns>
    public static Error UpdateUnexpectedError => new Error("AdoptionRequests.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the adoption request deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the adoption request deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("AdoptionRequests.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");

    /// <summary>
    /// Returns an error indicating that a failure occurred while retrieving adoption requests.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that a failure occurred during the retrieval of adoption requests.</returns>
    public static Error FailedToRetrieveAdoptionRequests => new Error("AdoptionRequests.FailedToRetrieveAdoptionRequests", "Failed to retrieve adoption requests.");
}


