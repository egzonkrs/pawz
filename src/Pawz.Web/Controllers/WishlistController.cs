using Microsoft.AspNetCore.Mvc;
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

    public async Task<IActionResult> Index()
    {
        var result = await _wishlistService.GetWishlistForUserAsync();

        if (!result.IsSuccess)
        {
            return BadRequest("Failed to get wishlist.");
        }

        return View(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddPetToWishlist(int petId)
    {
        var result = await _wishlistService.AddPetToWishlistAsync(petId);

        if (!result.IsSuccess)
        {
            return BadRequest("Failed to add pet to wishlist.");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemovePetFromWishlist(int petId)
    {
        var result = await _wishlistService.RemovePetFromWishlistAsync(petId);

        if (!result.IsSuccess)
        {
            return BadRequest("Failed to remove pet from wishlist.");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ClearWishlist(string id)
    {
        var result = await _wishlistService.DeleteWishlistAsync(id);

        if (!result.IsSuccess)
        {
            return BadRequest("Failed to clear wishlist.");
        }

        return RedirectToAction("Index");
    }
}
