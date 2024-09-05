using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Application.Models.Pet;
using Pawz.Domain.Entities;
using Pawz.Web.Extensions;
using Pawz.Web.Models.Breed;
using Pawz.Web.Models.City;
using Pawz.Web.Models.Pet;
using System.Collections.Generic;
using System.Linq;
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
    private readonly IValidator<AdoptionRequestCreateModel> _adoptionRequestValidator;
    private readonly IUserAccessor _userAccessor;
    private readonly IMapper _mapper;
    private readonly IAdoptionRequestService _adoptionRequestService;

    public PetController(
        IPetService petService,
        IBreedService breedService,
        ISpeciesService speciesService,
        ILocationService locationService,
        ICountryService countryService,
        ICityService cityService,
        IValidator<PetCreateViewModel> validator,
        IUserAccessor userAccessor,
        IMapper mapper,
        IAdoptionRequestService adoptionRequestService,
        IValidator<AdoptionRequestCreateModel> adoptionRequestValidator)
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
        _adoptionRequestService = adoptionRequestService;
        _adoptionRequestValidator = adoptionRequestValidator;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var pets = await _petService.GetAllPetsAsync(cancellationToken);
        return View(pets.Value);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var petResult = await _petService.GetPetByIdAsync(id, cancellationToken);
        var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
        var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

        var countriesList = countriesResult.Value ?? new List<Country>();
        var citiesList = citiesResult.Value ?? new List<City>();

        var adoptionRequestCreateModel = new AdoptionRequestCreateModel
        {
            PetId = id,
            Countries = new SelectList(countriesList, "Id", "Name"),
            Cities = new SelectList(citiesList, "Id", "Name"),
            AllCities = citiesList.Select(x => new CityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            }).ToList()
        };

        return View(adoptionRequestCreateModel);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var breedsResult = await _breedService.GetAllBreedsAsync(cancellationToken);
        var speciesResult = await _speciesService.GetAllSpeciesAsync(cancellationToken);
        var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
        var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

        var speciesList = speciesResult.Value ?? new List<Species>();
        var breedsList = breedsResult.Value ?? new List<Breed>();
        var countriesList = countriesResult.Value ?? new List<Country>();
        var citiesList = citiesResult.Value ?? new List<City>();

        var petCreateViewModel = new PetCreateViewModel
        {
            Species = new SelectList(speciesList, "Id", "Name"),
            Breeds = new SelectList(breedsList, "Id", "Name"),
            Countries = new SelectList(countriesList, "Id", "Name"),
            Cities = new SelectList(citiesList, "Id", "Name"),
            AllCities = citiesList.Select(x => new CityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            }).ToList(),
            AllBreeds = breedsList.Select(x => new BreedViewModel
            {
                Id = x.Id,
                Name = x.Name,
                SpeciesId = x.SpeciesId,
            }).ToList()
        };

        return View(petCreateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PetCreateViewModel petCreateViewModel, IEnumerable<IFormFile> imageFiles, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(petCreateViewModel, cancellationToken);

        if (validationResult.IsValid is false)
        {
            var breedsResult = await _breedService.GetAllBreedsAsync(cancellationToken);
            var speciesResult = await _speciesService.GetAllSpeciesAsync(cancellationToken);
            var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
            var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

            var speciesList = speciesResult.Value ?? new List<Species>();
            var breedsList = breedsResult.Value ?? new List<Breed>();
            var countriesList = countriesResult.Value ?? new List<Country>();
            var citiesList = citiesResult.Value ?? new List<City>();

            petCreateViewModel.Species = new SelectList(speciesList, "Id", "Name");
            petCreateViewModel.Breeds = new SelectList(breedsList, "Id", "Name");
            petCreateViewModel.Countries = new SelectList(countriesList, "Id", "Name");
            petCreateViewModel.Cities = new SelectList(citiesList, "Id", "Name");
            petCreateViewModel.AllBreeds = breedsList.Select(x => new BreedViewModel
            {
                Id = x.Id,
                Name = x.Name,
                SpeciesId = x.SpeciesId,
            }).ToList();

            validationResult.AddErrorsToModelState(ModelState);
            return View(petCreateViewModel);
        }

        var petCreateRequest = _mapper.Map<PetCreateRequest>(petCreateViewModel);
        petCreateRequest.ImageFiles = petCreateViewModel.ImageFiles;
        var petCreateResult = await _petService.CreatePetAsync(petCreateRequest, cancellationToken);

        if (petCreateResult.IsSuccess is false)
        {
            var breedsResult = await _breedService.GetAllBreedsAsync(cancellationToken);
            var speciesResult = await _speciesService.GetAllSpeciesAsync(cancellationToken);
            var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
            var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

            var speciesList = speciesResult.Value ?? new List<Species>();
            var breedsList = breedsResult.Value ?? new List<Breed>();
            var countriesList = countriesResult.Value ?? new List<Country>();
            var citiesList = citiesResult.Value ?? new List<City>();

            petCreateViewModel.Species = new SelectList(speciesList, "Id", "Name");
            petCreateViewModel.Breeds = new SelectList(breedsList, "Id", "Name");
            petCreateViewModel.Countries = new SelectList(countriesList, "Id", "Name");
            petCreateViewModel.Cities = new SelectList(citiesList, "Id", "Name");
            petCreateViewModel.AllBreeds = petCreateViewModel.AllBreeds = breedsList.Select(x => new BreedViewModel
            {
                Id = x.Id,
                Name = x.Name,
                SpeciesId = x.SpeciesId,
            }).ToList();

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

    public async Task<IActionResult> MyPets(CancellationToken cancellationToken)
    {
        var result = await _petService.GetPetsByUserIdAsync(cancellationToken);

        var pets = result.Value;

        var petResponses = _mapper.Map<IEnumerable<UserPetResponse>>(pets);
        return View(petResponses);
    }

    public IActionResult PetDetails()
    {
        return View();
    }
}
