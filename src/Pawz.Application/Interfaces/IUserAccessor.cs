namespace Pawz.Application.Interfaces;
public interface IUserAccessor
{
    string GetUserId();
    string GetEmail();
    string GetUserName();
    string GetUserRole();
    bool IsUserAuthenticated();
}
