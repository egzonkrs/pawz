using AutoMapper;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Web.Models;
using Pawz.Web.Models.Pet;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPetService _petService;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger,
        IPetService petService,
        IMapper mapper)
    {
        _logger = logger;
        _petService = petService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var result = await _petService.GetAllPetsAsync(cancellationToken);
        var petViewModels = _mapper.Map<IEnumerable<PetViewModel>>(result.Value);
        return View(petViewModels);
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
