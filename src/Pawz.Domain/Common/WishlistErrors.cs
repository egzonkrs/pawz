namespace Pawz.Domain.Common;

/// <summary>
/// Provides centralized error definitions related to wishlist operations.
/// </summary>
public static class WishlistErrors
{
    /// <summary>
    /// Returns an error indicating that a wishlist for the specified user ID was not found.
    /// </summary>
    /// <param name="userId">The ID of the user whose wishlist was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the wishlist for the specified user was not found.</returns>
    public static Error NotFound(string userId) => new Error("Wishlist.NotFound", $"Wishlist for user with ID {userId} was not found.");

    /// <summary>
    /// Returns an error indicating that the specified pet is already in the user's wishlist.
    /// </summary>
    /// <param name="petId">The ID of the pet that already exists in the wishlist.</param>
    /// <returns>An <see cref="Error"/> indicating that the pet is already in the wishlist.</returns>
    public static Error PetAlreadyInWishlist(int petId) => new Error("Wishlist.PetAlreadyInWishlist", $"Pet with ID {petId} is already in the wishlist.");

    /// <summary>
    /// Returns an error indicating that the specified pet is not in the user's wishlist.
    /// </summary>
    /// <param name="petId">The ID of the pet that is not in the wishlist.</param>
    /// <returns>An <see cref="Error"/> indicating that the pet is not in the wishlist.</returns>
    public static Error PetNotInWishlist(int petId) => new Error("Wishlist.PetNotInWishlist", $"Pet with ID {petId} is not in the wishlist.");

    /// <summary>
    /// Returns an error indicating that no changes were detected during the operation.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no changes were detected during the operation.</returns>
    public static Error NoChangesDetected => new Error("Wishlist.NoChanges", "No changes were detected during the operation.");

    /// <summary>
    /// Returns an error indicating that the wishlist could not be created due to a database failure.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the wishlist creation failed due to a database issue.</returns>
    public static Error CreationFailed => new Error("Wishlist.CreationFailed", "Wishlist creation failed. No changes were made to the database.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the wishlist creation process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during wishlist creation.</returns>
    public static Error CreationUnexpectedError => new Error("Wishlist.CreationUnexpectedError", "An unexpected error occurred during wishlist creation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the retrieval of the wishlist.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the retrieval of the wishlist.</returns>
    public static Error RetrievalError => new Error("Wishlist.RetrievalError", "An error occurred while retrieving the wishlist.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the update process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the update operation.</returns>
    public static Error UpdateUnexpectedError => new Error("Wishlist.UpdateUnexpectedError", "An unexpected error occurred during the update operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the wishlist deletion process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during the deletion process.</returns>
    public static Error DeletionUnexpectedError => new Error("Wishlist.DeletionUnexpectedError", "An unexpected error occurred during the deletion operation.");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred.</returns>
    public static Error UnexpectedError => new Error("Wishlist.UnexpectedError", "An unexpected error occurred.");
}
