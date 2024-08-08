using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers
{
    public class PetController : Controller
    {
        private readonly ILogger<PetController> _logger;
        private readonly IPetService _petService;
        public PetController(ILogger<PetController> logger, IPetService petService)
        {
            _logger = logger;
            _petService = petService;
        }

        public async Task<IActionResult> Index()
        {
            var pets = await _petService.GetAllPetsAsync();
            return View(pets);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pet = await _petService.GetPetByIdAsync(id);
            return View(pet);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pet pet)
        {
            await _petService.CreatePetAsync(pet);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pet = await _petService.GetPetByIdAsync(id);
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pet pet)
        {
            await _petService.UpdatePetAsync(pet);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _petService.GetPetByIdAsync(id);
            return View(pet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _petService.DeletePetAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
