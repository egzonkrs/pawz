using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Application.Models.Pet;
using Pawz.Web.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;
        private readonly IMapper _mapper;
        public PetController(IPetService petService, IMapper mapper)
        {
            _petService = petService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await _petService.GetAllPetsWithRelatedEntities(cancellationToken);

            var viewModel = _mapper.Map<IEnumerable<PetViewModel>>(result.Value);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var result = await _petService.GetPetByIdAsync(id, cancellationToken);

            var viewModel = _mapper.Map<PetViewModel>(result.Value);

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetViewModel petViewModel, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _petService.CreatePetAsync(_mapper.Map<PetRequest>(petViewModel), cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(petViewModel);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var result = await _petService.GetPetByIdAsync(id, cancellationToken);

            var viewModel = _mapper.Map<PetViewModel>(result.Value);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PetViewModel petViewModel, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _petService.UpdatePetAsync(id, _mapper.Map<PetRequest>(petViewModel), cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(petViewModel);
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _petService.GetPetByIdAsync(id, cancellationToken);

            var viewModel = _mapper.Map<PetViewModel>(result.Value);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, PetViewModel petViewModel, CancellationToken cancellationToken)
        {
            var petRequest = _mapper.Map<PetRequest>(petViewModel);

            await _petService.DeletePetAsync(id, cancellationToken);

            return RedirectToAction(nameof(Index));
        }
    }
}
