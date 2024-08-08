using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Pawz.Domain.Interfaces;
using Pawz.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Pawz.Web.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PetController(IPetRepository petRepository, IUnitOfWork unitOfWork)
        {
            _petRepository = petRepository;
            _unitOfWork = unitOfWork;
        }

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
        public async Task<IActionResult> Create([Bind("Id,Name,Breed,Age")] Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return View(pet);
            }

            await _petRepository.AddAsync(pet);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Breed,Age")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(pet);
            }

            try
            {
                await _petRepository.UpdateAsync(pet);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PetExists(pet.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
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
            if (pet != null)
            {
                await _petRepository.DeleteAsync(pet);
                await _unitOfWork.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PetExists(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            return pet != null;
        }
    }
}
