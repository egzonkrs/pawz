using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
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

public class PetController : Controller
{
    private readonly IPetService _petService;
    private readonly IBreedService _breedService;
    private readonly ISpeciesService _speciesService;
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;
    private readonly IValidator<PetCreateViewModel> _validator;
    private readonly IMapper _mapper;
    private readonly IAdoptionRequestService _adoptionRequestService;
    private readonly IUserAccessor _userAccessor;

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
        IAdoptionRequestService adoptionRequestService)
    {
        _petService = petService;
        _breedService = breedService;
        _speciesService = speciesService;
        _countryService = countryService;
        _cityService = cityService;
        _validator = validator;
        _mapper = mapper;
        _adoptionRequestService = adoptionRequestService;
        _userAccessor = userAccessor;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string searchTerm, CancellationToken cancellationToken)
    {
        IEnumerable<PetViewModel> petViewModels;

        if (string.IsNullOrEmpty(searchTerm))
        {
            var allPetsResult = await _petService.GetAllPetsWithRelatedEntities(cancellationToken);

            if (!allPetsResult.IsSuccess)
            {
                return View(new List<PetViewModel>());
            }

            petViewModels = _mapper.Map<IEnumerable<PetViewModel>>(allPetsResult.Value);
        }
        else
        {
            var searchResult = await _petService.SearchPetsByBreedAsync(searchTerm, cancellationToken);

            if (!searchResult.IsSuccess)
            {
                return View(new List<PetViewModel>());
            }

            petViewModels = _mapper.Map<IEnumerable<PetViewModel>>(searchResult.Value);
        }

        ViewBag.SearchTerm = searchTerm;

        return View(petViewModels);
    }


    [HttpGet("pet/details/{id:int}")]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
        var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);
        var countriesList = countriesResult.Value ?? new List<Country>();
        var citiesList = citiesResult.Value ?? new List<City>();

        var result = await _petService.GetPetByIdAsync(id, cancellationToken);

        if (!result.IsSuccess || result.Value == null)
        {
            return NotFound();
        }

        var petViewModel = _mapper.Map<PetViewModel>(result.Value);

        petViewModel.AdoptionRequestCreateModel = new AdoptionRequestCreateModel
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

        var requestResult = await _adoptionRequestService.HasUserMadeRequestForPetAsync(id, cancellationToken);

        petViewModel.HasExistingAdoptionRequest = requestResult.IsSuccess && requestResult.Value;
        return View(petViewModel);
    }

    [Authorize]
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

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PetCreateViewModel petCreateViewModel, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(petCreateViewModel, cancellationToken);

        var breedsResult = await _breedService.GetAllBreedsAsync(cancellationToken);
        var speciesResult = await _speciesService.GetAllSpeciesAsync(cancellationToken);
        var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
        var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

        var speciesList = speciesResult.Value ?? new List<Species>();
        var breedsList = breedsResult.Value ?? new List<Breed>();
        var countriesList = countriesResult.Value ?? new List<Country>();
        var citiesList = citiesResult.Value ?? new List<City>();

        if (validationResult.IsValid is false)
        {
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

            petCreateViewModel.AllCities = citiesList.Select(x => new CityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            }).ToList();

            validationResult.AddErrorsToModelState(ModelState);

            TempData["ErrorMessage"] = "Failed to create the pet! Please try again!";
            return View(petCreateViewModel);
        }

        var petCreateRequest = _mapper.Map<PetCreateRequest>(petCreateViewModel);
        var petCreateResult = await _petService.CreatePetAsync(petCreateRequest, cancellationToken);

        if (petCreateResult.IsSuccess is false)
        {
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

            petCreateViewModel.AllCities = citiesList.Select(x => new CityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            }).ToList();

            petCreateResult.AddErrorsToModelState(ModelState);
            return View(petCreateViewModel);
        }

        TempData["SuccessMessage"] = "Pet created successfully!";

        petCreateViewModel.Species = new SelectList(speciesList, "Id", "Name");
        petCreateViewModel.Breeds = new SelectList(breedsList, "Id", "Name");
        petCreateViewModel.Countries = new SelectList(countriesList, "Id", "Name");
        petCreateViewModel.Cities = new SelectList(citiesList, "Id", "Name");

        return RedirectToAction("Details", "Pet", new { id = petCreateResult.Value });
    }

    [Authorize]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var result = await _petService.GetPetByIdAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound();
        }

        var pet = result.Value;

        var breedsResult = await _breedService.GetAllBreedsAsync(cancellationToken);
        var breedsList = breedsResult.Value ?? new List<Breed>();

        var petEdit = new PetEditViewModel
        {
            Id = pet.Id,
            Name = pet.Name,
            BreedId = pet.BreedId,
            AgeYears = pet.AgeYears,
            About = pet.About,
            Price = pet.Price,
            Breeds = new SelectList(breedsList, "Id", "Name", pet.BreedId)
        };

        return View(petEdit);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Pet pet, CancellationToken cancellationToken)
    {
        await _petService.UpdatePetAsync(pet, cancellationToken);
        return RedirectToAction("Profile", "Users");
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var petResult = await _petService.GetPetByIdAsync(id, cancellationToken);
        if (!petResult.IsSuccess)
        {
            return NotFound();
        }
        return View(petResult.Value);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var userId = _userAccessor.GetUserId();
        var result = await _petService.DeletePetAsync(id, userId, cancellationToken);

        if (!result.IsSuccess)
        {
            result.AddErrorsToModelState(ModelState);
            return View();
        }
        return RedirectToAction("Profile", "Users");
    }
}

