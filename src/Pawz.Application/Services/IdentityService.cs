using Microsoft.AspNetCore.Identity;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
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

    public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        return result;
    }
}
