using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers
{
    [Route("[controller]")]
    public class BreedController : Controller
    {
        private readonly IBreedService _breedService;
        public BreedController(IBreedService breedService)
        {
            _breedService = breedService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var breeds = await _breedService.GetAllBreedsAsync(cancellationToken);
            return View(breeds);
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var breed = await _breedService.GetBreedByIdAsync(id, cancellationToken);
            return View(breed);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Breed breed, CancellationToken cancellationToken)
        {
            await _breedService.CreateBreedAsync(breed, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var breed = await _breedService.GetBreedByIdAsync(id, cancellationToken);
            return View(breed);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Breed breed, CancellationToken cancellationToken)
        {
            await _breedService.UpdateBreedAsync(breed, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var breed = await _breedService.GetBreedByIdAsync(id, cancellationToken);
            return View(breed);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            await _breedService.DeleteBreedAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}