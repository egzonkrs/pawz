using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Web.Models.Wishlist;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class WishlistController : Controller
{
    private readonly IWishlistService _wishlistService;
    private readonly IMapper _mapper;
    public WishlistController(IWishlistService wishlistService, IMapper mapper)
    {
        _wishlistService = wishlistService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _wishlistService.GetWishlistForUserAsync();

        if (!result.IsSuccess)
        {
            return BadRequest("Failed to get wishlist.");
        }

        var wishlistViewModel = _mapper.Map<WishlistViewModel>(result.Value);

        return View(wishlistViewModel);
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
