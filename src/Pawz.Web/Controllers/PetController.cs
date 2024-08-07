using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers
{
    [Route("[controller]")]
    public class PetController(IPetRepository petRepository) : Controller
    {
        private readonly IPetRepository _petRepository = petRepository;

        public async Task<IActionResult> Index()
        {
            var pets = await _petRepository.GetAllAsync();
            return View(pets);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
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
            if (ModelState.IsValid)
            {
                await _petRepository.AddAsync(pet);
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pet pet)
        {
            if (id != pet.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _petRepository.UpdateAsync(pet);
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            await _petRepository.DeleteAsync(pet);
            return RedirectToAction(nameof(Index));
        }
    }
}