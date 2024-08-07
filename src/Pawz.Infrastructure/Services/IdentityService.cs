using Microsoft.AspNetCore.Identity;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
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
        var result = await _userManager.CreateAsync(user, password);
        return result;
    }
}
