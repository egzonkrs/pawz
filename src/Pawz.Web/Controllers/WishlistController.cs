using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _wishlistService.GetWishlistForUserAsync();

        return View("Index", result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist(int petId)
    {
        var result = await _wishlistService.AddPetToWishlistAsync(petId);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }

        return View("Index", result.Value);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFromWishlist(int petId)
    {
        var result = await _wishlistService.RemovePetFromWishlistAsync(petId);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }

        return View("Index", result.Value);
    }
}
