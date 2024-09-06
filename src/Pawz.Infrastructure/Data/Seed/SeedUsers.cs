using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedUsers
{
    private readonly UserManager<ApplicationUser> _userManager;
    private const string SeedUsersPassword = "Password@123";

    public SeedUsers(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public static async Task SeedUserData(UserManager<ApplicationUser> userManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var userAsd = new ApplicationUser
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "asd@qwe.com",
            FirstName = "Tester",
            LastName = "Maverick",
        };

        var userJohn = new ApplicationUser
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "john@example.com",
            FirstName = "john",
            LastName = "doe"
        };

        var userBob = new ApplicationUser
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "bob@example.com",
            FirstName = "bob",
            LastName = "doe",
            IsDeleted = false,
            Address = "123 Main St"
        };

        var userJane = new ApplicationUser
        {
            UserName = Guid.NewGuid().ToString(),
            Email = "jane@example.com",
            FirstName = "jane",
            LastName = "doe"
        };

        await userManager.CreateAsync(userJohn, SeedUsersPassword);
        await userManager.CreateAsync(userJane, SeedUsersPassword);
        await userManager.CreateAsync(userBob, SeedUsersPassword);
        await userManager.CreateAsync(userAsd, "asdqwe123$A");
    }
}
