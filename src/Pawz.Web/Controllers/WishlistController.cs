using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Pawz.Application.Interfaces;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class WishlistController : Controller
{
    private readonly IWishlistService _wishlistService;
    private readonly IUserAccessor _userAccessor;

    public WishlistController(IWishlistService wishlistService, IUserAccessor userAccessor)
    {
        _wishlistService = wishlistService;
        _userAccessor = userAccessor;
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist(int petId)
    {
        var loggedinUserId = _userAccessor.GetUserId();

        var result = await _wishlistService.AddPetToWishlistAsync(loggedinUserId, petId);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }

        return View("Index", result.Value);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFromWishlist(int petId)
    {
        var loggedinUserId = _userAccessor.GetUserId();

        var result = await _wishlistService.RemovePetFromWishlistAsync(loggedinUserId, petId);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }

        return View("Index", result.Value);
    }
}
