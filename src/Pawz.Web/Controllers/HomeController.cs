using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Helpers;
using Pawz.Web.Models;
using Pawz.Web.Models.Pet;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class HomeController : Controller
{
    private readonly IPetService _petService;
    private readonly IMapper _mapper;

    public HomeController(IPetService petService, IMapper mapper)
    {
        _petService = petService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(QueryParams queryParams, CancellationToken cancellationToken)
    {
        var result = await _petService.GetAvailablePetsWithDetailsAsync(queryParams, cancellationToken);

        if (!result.IsSuccess)
        {
            return View("Error");
        }

        var petViewModels = _mapper.Map<IEnumerable<PetViewModel>>(result.Value.Pets);

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
