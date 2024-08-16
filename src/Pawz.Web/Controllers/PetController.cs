using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;
        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var pets = await _petService.GetAllPetsAsync(cancellationToken);
            return View(pets);
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var pet = await _petService.GetPetByIdAsync(id, cancellationToken);
            return View(pet);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pet pet, CancellationToken cancellationToken)
        {
            await _petService.CreatePetAsync(pet, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var pet = await _petService.GetPetByIdAsync(id, cancellationToken);
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pet pet, CancellationToken cancellationToken)
        {
            await _petService.UpdatePetAsync(pet, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var pet = await _petService.GetPetByIdAsync(id, cancellationToken);
            return View(pet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
        {
            await _petService.DeletePetAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
