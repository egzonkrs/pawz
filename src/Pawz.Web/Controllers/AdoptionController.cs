using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly IAdoptionService _adoptionService;
        public AdoptionController(IAdoptionService _adoptionService)
        {
            _adoptionService = _adoptionService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var adoptions = await _adoptionService.GetAllAdoptionsAsync(cancellationToken);
            return View(adoptions);
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var adoption = await _adoptionService.GetAdoptionByIdAsync(id, cancellationToken);
            return View(adoption);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Adoption adoption, CancellationToken cancellationToken)
        {
            await _adoptionService.CreateAdoptionAsync(adoption, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var adoption = await _adoptionService.GetAdoptionByIdAsync(id, cancellationToken);
            return View(adoption);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Adoption adoption, CancellationToken cancellationToken)
        {
            await _adoptionService.UpdateAdoptionAsync(adoption, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var adoption = await _adoptionService.GetAdoptionByIdAsync(id, cancellationToken);
            return View(adoption);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            await _adoptionService.DeleteAdoptionAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
