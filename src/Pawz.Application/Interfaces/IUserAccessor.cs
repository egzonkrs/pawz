namespace Pawz.Application.Interfaces;

/// <summary>
/// Interface to access user-specific information from the current HTTP context.
/// </summary>
public interface IUserAccessor
{
    /// <summary>
    /// Gets the user's unique identifier (ID).
    /// </summary>
    /// <returns>The user's ID as a string.</returns>
    string GetUserId();

    /// <summary>
    /// Gets the user's email address.
    /// </summary>
    /// <returns>The user's email address as a string.</returns>
    string GetEmail();

    /// <summary>
    /// Gets the user's name.
    /// </summary>
    /// <returns>The user's name as a string.</returns>
    string GetUserName();

    /// <summary>
    /// Gets the user's role.
    /// </summary>
    /// <returns>The user's role as a string.</returns>
    string GetUserRole();

    /// <summary>
    /// Determines whether the user is authenticated.
    /// </summary>
    /// <returns>True if the user is authenticated, otherwise false.</returns>
    bool IsUserAuthenticated();

    /// <summary>
    /// Retrieves specific information about the currently logged-in user based on the provided info type.
    /// </summary>
    /// <returns>
    /// A string containing the requested information (e.g., profile picture URL, address, phone number),
    /// or null if the information is not available or the infoType is invalid.
    /// </returns>
    string GetUserInfo(string infoType);

}
