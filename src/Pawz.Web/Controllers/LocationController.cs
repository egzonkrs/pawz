using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;
public class LocationController : Controller
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var locations = await _locationService.GetAllLocationsAsync(cancellationToken);
        return View(locations);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var location = await _locationService.GetLocationByIdAsync(id, cancellationToken);
        return View(location);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Location location, CancellationToken cancellationToken)
    {
        await _locationService.CreateLocationAsync(location, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var location = await _locationService.GetLocationByIdAsync(id, cancellationToken);
        return View(location);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Location location, CancellationToken cancellationToken)
    {
        await _locationService.UpdateLocationAsync(location, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var location = await _locationService.GetLocationByIdAsync(id, cancellationToken);
        return View(location);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        await _locationService.DeleteLocationAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
