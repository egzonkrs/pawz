using Microsoft.AspNetCore.Identity;
using Pawz.Domain.Entities;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedUsers
{
    private readonly UserManager<ApplicationUser> _userManager;

    public SeedUsers(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public static async Task SeedUserData(UserManager<ApplicationUser> userManager)
    {
        var userJohn = new ApplicationUser
        {
            UserName = "john",
            Email = "john@example.com",
            FirstName = "john",
            LastName = "doe",
            ImageUrl = "https://api.dicebear.com/9.x/micah/svg?seed=Bandit"
        };

        var userBob = new ApplicationUser
        {
            UserName = "bob",
            Email = "bob@example.com",
            FirstName = "bob",
            LastName = "doe",
            IsDeleted = false,
            Address = "123 Main St",
            ImageUrl = "https://api.dicebear.com/9.x/micah/svg?seed=Loki"
        };

        var userJane = new ApplicationUser
        {
            UserName = "jane",
            Email = "jane@example.com",
            FirstName = "jane",
            LastName = "doe",
            IsDeleted = false,
            Address = "123 Main St",
            ImageUrl = ""
        };

        var userNina = new ApplicationUser
        {
            UserName = "nina",
            Email = "nina@example.com",
            FirstName = "nina",
            LastName = "doe",
            IsDeleted = false,
            Address = "123 Main St",
            ImageUrl = ""
        };

        await userManager.CreateAsync(userJohn, "Password@123");
        await userManager.CreateAsync(userJane, "Password@123");
        await userManager.CreateAsync(userBob, "Password@123");
        await userManager.CreateAsync(userNina, "Password@123");
    }
}
