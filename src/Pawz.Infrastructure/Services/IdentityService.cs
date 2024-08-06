using Microsoft.AspNetCore.Identity;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public Task LoginAsync()
    {
        //TODO:
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User cannot be null!");

        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password), "Password cannot be null or empty!");

        var result = await _userManager.CreateAsync(user, password);
        return result;
    }
}
