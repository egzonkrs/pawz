using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Application.Models.Pet;
using Pawz.Domain.Entities;
using Pawz.Web.Extensions;
using Pawz.Web.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

[Authorize]
public class PetController : Controller
{
    private readonly IPetService _petService;
    private readonly IBreedService _breedService;
    private readonly ISpeciesService _speciesService;
    private readonly ILocationService _locationService;
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;
    private readonly IValidator<PetCreateViewModel> _validator;
    private readonly IUserAccessor _userAccessor;
    private readonly IMapper _mapper;

    public PetController(IPetService petService, IBreedService breedService, ISpeciesService speciesService, ILocationService locationService,
        ICountryService countryService, ICityService cityService, IValidator<PetCreateViewModel> validator, IUserAccessor userAccessor, IMapper mapper)
    {
        _petService = petService;
        _breedService = breedService;
        _speciesService = speciesService;
        _locationService = locationService;
        _countryService = countryService;
        _cityService = cityService;
        _validator = validator;
        _userAccessor = userAccessor;
        _mapper = mapper;
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

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var breedsResult = await _breedService.GetAllBreedsAsync(cancellationToken);
        var speciesResult = await _speciesService.GetAllSpeciesAsync(cancellationToken);
        var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
        var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

        var petCreateViewModel = new PetCreateViewModel
        {
            Breeds = new SelectList(breedsResult.Value, "Id", "Name"),
            Species = new SelectList(speciesResult.Value, "Id", "Name"),
            Countries = new SelectList(countriesResult.Value, "Id", "Name"),
            Cities = new SelectList(citiesResult.Value, "Id", "Name"),
            Locations = new SelectList(new List<Location>(), "Id", "Address")
        };

        return View(petCreateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PetCreateViewModel petCreateViewModel, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(petCreateViewModel, cancellationToken);

        if (!validationResult.IsValid)
        {
            var breedsResult = await _breedService.GetAllBreedsAsync(cancellationToken);
            var speciesResult = await _speciesService.GetAllSpeciesAsync(cancellationToken);
            var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
            var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

            petCreateViewModel.Breeds = new SelectList(breedsResult.Value, "Id", "Name");
            petCreateViewModel.Species = new SelectList(speciesResult.Value, "Id", "Name");
            petCreateViewModel.Countries = new SelectList(countriesResult.Value, "Id", "Name");
            petCreateViewModel.Cities = new SelectList(citiesResult.Value, "Id", "Name");

            validationResult.AddErrorsToModelState(ModelState);
            return View(petCreateViewModel);
        }

        // Create Location first
        var location = new Location
        {
            CityId = petCreateViewModel.CityId,
            Address = petCreateViewModel.Address,
            PostalCode = petCreateViewModel.PostalCode
        };

        var locationInsertResult = await _locationService.CreateLocationAsync(location, cancellationToken);

        if (!locationInsertResult.IsSuccess)
        {
            ModelState.AddModelError("", "Failed to create location.");
            return View(petCreateViewModel);
        }

        // Retrieve the created Location's Id
        var createdLocation = locationInsertResult.Value;
        if (createdLocation == null)
        {
            ModelState.AddModelError("", "Failed to retrieve created location.");
            return View(petCreateViewModel);
        }

        var petCreateRequest = _mapper.Map<PetCreateRequest>(petCreateViewModel);
        petCreateRequest.PostedByUserId = _userAccessor.GetUserId();
        petCreateRequest.LocationId = createdLocation.Id;

        var petCreateResult = await _petService.CreatePetAsync(petCreateRequest, cancellationToken);

        if (!petCreateResult.IsSuccess)
        {
            ModelState.AddModelError("", "Failed to create pet.");
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

    public async Task<IActionResult> MyPets(CancellationToken cancellationToken)
    {
        var result = await _petService.GetPetsByUserIdAsync(cancellationToken);

        var pets = result.Value;

        var petResponses = _mapper.Map<IEnumerable<UserPetResponse>>(pets);
        return View(petResponses);
    }
}

