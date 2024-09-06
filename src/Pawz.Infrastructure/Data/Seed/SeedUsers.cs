using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed
{
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

            await CreateUserWithClaims(userManager, userAsd, "asdqwe123$A");
            await CreateUserWithClaims(userManager, userJohn, SeedUsersPassword);
            await CreateUserWithClaims(userManager, userBob, SeedUsersPassword);
            await CreateUserWithClaims(userManager, userJane, SeedUsersPassword);
        }

        private static async Task CreateUserWithClaims(UserManager<ApplicationUser> userManager, ApplicationUser user, string password)
        {
            var createUserResult = await userManager.CreateAsync(user, password);

            if (createUserResult.Succeeded)
            {
                var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Role, "User")
                };

                var claimResult = await userManager.AddClaimsAsync(user, userClaims);

                if (!claimResult.Succeeded)
                {
                    throw new Exception();
                }
            }
        }
    }
}
