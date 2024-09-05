using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Web.Models;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPetService _petService;

    public HomeController(ILogger<HomeController> logger, IPetService petService)
    {
        _logger = logger;
        _petService = petService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        // Fetch the list of pets from the service
        var pets = await _petService.GetAllPetsAsync(cancellationToken);

        // Pass the list of pets to the view
        return View(pets.Value);
    }

    public IActionResult Adopt()
    {
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }

    public IActionResult WishList()
    {
        return View();
    }

    public IActionResult ContactUs()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
