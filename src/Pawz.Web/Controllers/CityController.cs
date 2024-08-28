using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;
public class CityController : Controller
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var cities = await _cityService.GetAllCitiesAsync(cancellationToken);
        return View(cities);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var city = await _cityService.GetCityByIdAsync(id, cancellationToken);
        return View(city);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(City city, CancellationToken cancellationToken)
    {
        await _cityService.CreateCityAsync(city, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var city = await _cityService.GetCityByIdAsync(id, cancellationToken);
        return View(city);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(City city, CancellationToken cancellationToken)
    {
        await _cityService.UpdateCityAsync(city, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var city = await _cityService.GetCityByIdAsync(id, cancellationToken);
        return View(city);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        await _cityService.DeleteCityAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
