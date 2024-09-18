namespace Pawz.Application.Models.UserModel;
public class EditUserRequest
{
    public string UserId { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
}
