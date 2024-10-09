using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Pawz.Application.Interfaces;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class WishlistController : Controller
{
    private readonly IWishlistService _wishlistService;
    private readonly ILogger<WishlistController> _logger;
    public WishlistController(IWishlistService wishlistService, ILogger<WishlistController> logger)
    {
        _wishlistService = wishlistService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _wishlistService.GetWishlistForUserAsync();

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }

        // Log or inspect result.Value before returning it to the view
        _logger.LogInformation($"Fetched wishlist: {JsonSerializer.Serialize(result.Value)}");

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
