using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;
public class UsersController : Controller
{
    private readonly IIdentityService _identityService;

    public UsersController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }
}
