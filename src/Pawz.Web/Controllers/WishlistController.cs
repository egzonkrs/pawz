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
        var wishlist = await _wishlistService.GetWishlistAsync(key);

        return View("Index", wishlist ?? new Wishlist{Id = key});
    }

    [HttpPost]
    public async Task<ActionResult<Wishlist>> UpdateWishlist(Wishlist wishlist)
    {
        var updatedWishlist = await _wishlistService.SetWishlistAsync(wishlist);

        if (updatedWishlist == null) return BadRequest("Problem with wishlist");

        return View("Index", updatedWishlist);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteWishlist(string id)
    {
        var result = await _wishlistService.DeleteWishlistAsync(id);

        if (!result) return BadRequest("Problem deleting wishlist");

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> TestRedisConnection()
    {
        var isRedisConnected = await _wishlistService.TestRedisConnectionAsync();
        if (isRedisConnected)
        {
            return Json(new { Success = true, Message = "Redis connection is healthy." });
        }
        else
        {
            return Json(new { Success = false, Message = "Failed to connect to Redis." });
        }
    }
}
