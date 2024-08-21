using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Domain.Entities;
using Pawz.Web.Extensions;
using Pawz.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        private readonly IPetService _petService;
        private readonly ISpeciesService _speciesService;
        private readonly IBreedService _breedService;
        private readonly IValidator<PetCreateViewModel> _validator;
        public PetController(IPetService petService, ISpeciesService speciesService, IBreedService breedService, IValidator<PetCreateViewModel> validator)
        {
            _petService = petService;
            _speciesService = speciesService;
            _breedService = breedService;
            _validator = validator;
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

        [Authorize]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var petCreateViewModel = new PetCreateViewModel
            {
                Species = await GetSpeciesSelectListItemsAsync(cancellationToken),
                Breeds = await GetBreedsSelectListItemsAsync(cancellationToken)
            };

            return View(petCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetCreateViewModel petCreateViewModel, CancellationToken cancellationToken)
        {
            petCreateViewModel.Species = await GetSpeciesSelectListItemsAsync(cancellationToken);
            petCreateViewModel.Breeds = await GetBreedsSelectListItemsAsync(cancellationToken);

            var validationResult = await _validator.ValidateAsync(petCreateViewModel);

            if (validationResult.IsValid is false)
            {
                validationResult.AddErrorsToModelState(ModelState);
                return View(petCreateViewModel);
            }

            var petCreateRequest = new PetCreateRequest
            {
                Name = petCreateViewModel.Name,
                SpeciesId = petCreateViewModel.SpeciesId,
                BreedId = petCreateViewModel.BreedId,
                AgeYears = petCreateViewModel.AgeYears,
                AgeMonths = petCreateViewModel.AgeMonths,
                About = petCreateViewModel.About,
                Price = petCreateViewModel.Price,
                LocationId = petCreateViewModel.LocationId,
                Status = petCreateViewModel.Status,
                PostedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            var petCreateResult = await _petService.CreatePetAsync(petCreateRequest, cancellationToken);

            if (petCreateResult.IsSuccess is false)
            {
                petCreateResult.AddErrorsToModelState(ModelState);
                return View(petCreateViewModel);
            }

            return RedirectToAction("Index", "Home");
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

        private async Task<IEnumerable<SelectListItem>> GetSpeciesSelectListItemsAsync(CancellationToken cancellationToken)
        {
            var result = await _speciesService.GetAllSpeciesAsync(cancellationToken);
            if (result.IsSuccess)
            {
                return result.Value.Select(species => new SelectListItem
                {
                    Text = species.Name,
                    Value = species.Id.ToString(),
                });
            }
            return Enumerable.Empty<SelectListItem>();
        }

        private async Task<IEnumerable<SelectListItem>> GetBreedsSelectListItemsAsync(CancellationToken cancellationToken)
        {
            var result = await _breedService.GetAllBreedsAsync(cancellationToken);
            if (result.IsSuccess)
            {
                return result.Value.Select(breed => new SelectListItem
                {
                    Value = breed.Id.ToString(),
                    Text = breed.Name
                });
            }
            return Enumerable.Empty<SelectListItem>();
        }


    }
}
