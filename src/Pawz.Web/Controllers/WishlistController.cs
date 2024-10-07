using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System.Collections.Generic;
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
    public async Task<ActionResult<Wishlist>> GetWishlist(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            ViewBag.Message = "No key provided";
            return Json(new Wishlist { Id = key, Pets = new List<Pet>() });
        }

        var wishlist = await _wishlistService.GetWishlistAsync(key);

        if (wishlist == null)
        {
            ViewBag.Message = "No items in wishlist";
            return Json(new Wishlist { Id = key, Pets = new List<Pet>() });
        }

        return Json(wishlist);
    }


    [HttpPost]
    public async Task<ActionResult<Wishlist>> UpdateWishlist(Wishlist wishlist)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _wishlistService.SetWishlistAsync(wishlist);

        if (result == null)
        {
            return StatusCode(500, "Failed to update wishlist");
        }

        return View("Index", wishlist);
    }

    [HttpPost]
    public async Task<ActionResult> DeleteWishlist(string id)
    {
        var deleted = await _wishlistService.DeleteWishlistAsync(id);

        if (!deleted)
        {
            return BadRequest("Problem deleting wishlist");
        }

        return RedirectToAction(nameof(WishlistDeletedConfirmation));
    }

    [HttpGet]
    public IActionResult WishlistDeletedConfirmation()
    {
        return View();
    }
}
