using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;
public class CountryController : Controller
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var countries = await _countryService.GetAllCountriesAsync(cancellationToken);
        return View(countries);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var country = await _countryService.GetCountryByIdAsync(id, cancellationToken);
        return View(country);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Country country, CancellationToken cancellationToken)
    {
        await _countryService.CreateCountryAsync(country, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var country = await _countryService.GetCountryByIdAsync(id, cancellationToken);
        return View(country);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Country country, CancellationToken cancellationToken)
    {
        await _countryService.UpdateCountryAsync(country, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var country = await _countryService.GetCountryByIdAsync(id, cancellationToken);
        return View(country);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        await _countryService.DeleteCountryAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetCountries(CancellationToken cancellationToken)
    {
        var result = await _countryService.GetAllCountriesAsync(cancellationToken);
        if (result.IsSuccess)
        {
            return Json(result.Value); // Return the list of countries as JSON
        }
        return StatusCode(500, "An error occurred while retrieving countries.");
    }
}
