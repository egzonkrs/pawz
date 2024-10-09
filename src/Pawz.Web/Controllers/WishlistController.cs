using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class WishlistController : Controller
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist(int petId)
    {
        var userId = User.FindFirst("sub")?.Value;

        var result = await _wishlistService.AddPetToWishlistAsync(userId, petId);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFromWishlist(int petId)
    {
        var userId = User.FindFirst("sub")?.Value;

        var result = await _wishlistService.RemovePetFromWishlistAsync(userId, petId);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }
}
