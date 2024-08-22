using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers
{
    [Route("[controller]")]
    public class SpeciesController : Controller
    {
        private readonly ISpeciesService _speciesService;
        public SpeciesController(ISpeciesService speciesService)
        {
            _speciesService = speciesService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var species = await _speciesService.GetAllSpeciesAsync(cancellationToken);
            return View(species);
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var species = await _speciesService.GetSpeciesByIdAsync(id, cancellationToken);
            return View(species);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Species species, CancellationToken cancellationToken)
        {
            await _speciesService.CreateSpeciesAsync(species, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var species = await _speciesService.GetSpeciesByIdAsync(id, cancellationToken);
            return View(species);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Species species, CancellationToken cancellationToken)
        {
            await _speciesService.UpdateSpeciesAsync(species, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var species = await _speciesService.GetSpeciesByIdAsync(id, cancellationToken);
            return View(species);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            await _speciesService.DeleteSpeciesAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}