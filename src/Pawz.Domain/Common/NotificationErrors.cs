namespace Pawz.Domain.Common;

/// <summary>
/// Provides centralized error definitions related to notification operations.
/// </summary>
public class NotificationErrors
{
    /// <summary>
    /// Returns an error indicating that a notification with the specified ID was not found.
    /// </summary>
    /// <param name="id">The ID of the notification that was not found.</param>
    /// <returns>An <see cref="Error"/> indicating that the notification was not found.</returns>
    public static Error NotFound(int id) => new Error("Notifications.NotFound", $"Notification with ID {id} was not found.");

    /// <summary>
    /// Returns an error indicating that no notifications were found.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that no notifications were found.</returns>
    public static Error NoNotificationsFound => new Error("Notifications.NoNotificationsFound", "No notifications were found.");

    /// <summary>
    /// Returns an error indicating that sending a notification to a specific user failed.
    /// </summary>
    /// <param name="userId">The ID of the user to whom the notification failed to send.</param>
    /// <returns>An <see cref="Error"/> indicating that the notification sending failed.</returns>
    public static Error SendingFailed(string userId) => new Error("Notification.SendingFailed", $"Failed to send notification to user with ID: {userId}");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during the notification sending process.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred during notification sending.</returns>
    public static Error SendingUnexpectedError => new Error("Notifications.SendingUnexpectedError", "An unexpected error occurred while sending the notification.");

    /// <summary>
    /// Returns an error indicating that the notification retrieval failed.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the notification retrieval failed.</returns>
    public static Error RetrievalError => new Error("Notifications.RetrievalError", "An error occurred while retrieving notifications.");

    /// <summary>
    /// Returns an error indicating that the notification creation failed.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the notification creation failed.</returns>
    public static Error CreationFailed => new Error("Notifications.CreationFailed", "Failed to create the notification.");

    /// <summary>
    /// Returns an error indicating that establishing a user connection failed.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the user connection failed.</returns>
    public static Error UserConnectionFailed => new Error("Notification.UserConnectionFailed", "Failed to establish user connection");

    /// <summary>
    /// Returns an error indicating that an error occurred during the disconnection of a user.
    /// </summary>
    /// <param name="userId">The ID of the user who was disconnected.</param>
    /// <param name="connectionId">The connection ID associated with the disconnection.</param>
    /// <returns>An <see cref="Error"/> indicating that a disconnection error occurred for the user with the provided user ID and connection ID.</returns>
    public static Error DisconnectionError(string userId, string connectionId) =>
    new Error("Notification.DisconnectionError", $"An error occurred during the disconnection of user with ID: {userId} and connection ID: {connectionId}. ");

    /// <summary>
    /// Returns an error indicating that handling a user disconnection failed.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that handling the user disconnection failed.</returns>
    public static Error UserDisconnectionFailed => new Error("Notification.UserDisconnectionFailed", "Failed to handle user disconnection");

    /// <summary>
    /// Returns an error indicating that an unexpected error occurred during notification processing.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that an unexpected error occurred.</returns>
    public static Error UnexpectedError => new Error("Notification.UnexpectedError", "An unexpected error occurred while processing the notification");

    /// <summary>
    /// Returns an error indicating that the recipient of the notification is invalid or not specified.
    /// </summary>
    /// <returns>An <see cref="Error"/> indicating that the recipient is invalid.</returns>
    public static Error InvalidRecipient => new Error("Notification.InvalidRecipient", "The recipient of the notification is invalid or not specified");
}
