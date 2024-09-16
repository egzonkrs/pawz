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

// [Authorize]
public class PetController : Controller
{
    private readonly IPetService _petService;
    private readonly IBreedService _breedService;
    private readonly ISpeciesService _speciesService;
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;
    private readonly IValidator<PetCreateViewModel> _validator;
    private readonly IValidator<AdoptionRequestCreateModel> _adoptionRequestValidator;
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
        _countryService = countryService;
        _cityService = cityService;
        _validator = validator;
        _mapper = mapper;
        _adoptionRequestService = adoptionRequestService;
        _adoptionRequestValidator = adoptionRequestValidator;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var result = await _petService.GetAllPetsWithRelatedEntities(cancellationToken);
        var petViewModels = _mapper.Map<IEnumerable<PetViewModel>>(result.Value);
        return View(petViewModels);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
        var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

        var countriesList = countriesResult.Value ?? new List<Country>();
        var citiesList = citiesResult.Value ?? new List<City>();

        var result = await _petService.GetPetByIdAsync(id, cancellationToken);
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

        return View(petViewModel);
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

        if (petCreateResult.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }

        petCreateViewModel.Species = new SelectList(speciesList, "Id", "Name");
        petCreateViewModel.Breeds = new SelectList(breedsList, "Id", "Name");
        petCreateViewModel.Countries = new SelectList(countriesList, "Id", "Name");
        petCreateViewModel.Cities = new SelectList(citiesList, "Id", "Name");

        TempData["SuccessMessage"] = "Pet created successfully!";
        return RedirectToAction("Details", "Pet");
    }

    //public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    //{
    //    // Merr pet-in ekzistues nga baza e të dhënave
    //    var petResult = await _petService.GetPetByIdAsync(id, cancellationToken);
    //    if (petResult.IsSuccess is false)
    //    {
    //        TempData["ErrorMessage"] = "Pet not found!";
    //        return RedirectToAction("Index", "Pet");
    //    }

    //    // Merr të dhënat që janë të nevojshme për formën
    //    var breedsResult = await _breedService.GetAllBreedsAsync(cancellationToken);
    //    var speciesResult = await _speciesService.GetAllSpeciesAsync(cancellationToken);
    //    var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
    //    var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

    //    var petCreateViewModel = new PetCreateViewModel
    //    {
    //        Id = petResult.Value.Id,
    //        Name = petResult.Value.Name,
    //        SpeciesId = petResult.Value.SpeciesId,
    //        BreedId = petResult.Value.BreedId,
    //        AgeYears = petResult.Value.AgeYears,
    //        Price = petResult.Value.Price,
    //        CountryId = petResult.Value.Location.City.CountryId,
    //        CityId = petResult.Value.Location.CityId,
    //        PostalCode = petResult.Value.Location.PostalCode,
    //        Address = petResult.Value.Location.Address,
    //        About = petResult.Value.About,
    //        Species = new SelectList(speciesResult.Value ?? new List<Species>(), "Id", "Name"),
    //        Breeds = new SelectList(breedsResult.Value ?? new List<Breed>(), "Id", "Name"),
    //        Countries = new SelectList(countriesResult.Value ?? new List<Country>(), "Id", "Name"),
    //        Cities = new SelectList(citiesResult.Value ?? new List<City>(), "Id", "Name"),
    //        AllBreeds = breedsResult.Value?.Select(x => new BreedViewModel { Id = x.Id, Name = x.Name, SpeciesId = x.SpeciesId }).ToList(),
    //        AllCities = citiesResult.Value?.Select(x => new CityViewModel { Id = x.Id, Name = x.Name, CountryId = x.CountryId }).ToList()
    //    };

    //    return View(petCreateViewModel);
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(PetCreateViewModel petCreateViewModel, IEnumerable<IFormFile> imageFiles, CancellationToken cancellationToken)
    //{
    //    // Validimi i input-it
    //    var validationResult = await _validator.ValidateAsync(petCreateViewModel, cancellationToken);
    //    if (!validationResult.IsValid)
    //    {
    //        validationResult.AddErrorsToModelState(ModelState);

    //        // Rimbush formën me të dhënat aktuale në rast gabimi
    //        var breedsResult = await _breedService.GetAllBreedsAsync(cancellationToken);
    //        var speciesResult = await _speciesService.GetAllSpeciesAsync(cancellationToken);
    //        var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
    //        var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

    //        petCreateViewModel.Species = new SelectList(speciesResult.Value ?? new List<Species>(), "Id", "Name");
    //        petCreateViewModel.Breeds = new SelectList(breedsResult.Value ?? new List<Breed>(), "Id", "Name");
    //        petCreateViewModel.Countries = new SelectList(countriesResult.Value ?? new List<Country>(), "Id", "Name");
    //        petCreateViewModel.Cities = new SelectList(citiesResult.Value ?? new List<City>(), "Id", "Name");
    //        petCreateViewModel.AllBreeds = breedsResult.Value?.Select(x => new BreedViewModel { Id = x.Id, Name = x.Name, SpeciesId = x.SpeciesId }).ToList();
    //        petCreateViewModel.AllCities = citiesResult.Value?.Select(x => new CityViewModel { Id = x.Id, Name = x.Name, CountryId = x.CountryId }).ToList();

    //        TempData["ErrorMessage"] = "Failed to update the pet! Please try again!";
    //        return View(petCreateViewModel);
    //    }

    //    // Përditëson pet-in
    //    var petUpdateRequest = _mapper.Map<PetCreateRequest>(petCreateViewModel);
    //    petUpdateRequest.ImageFiles = imageFiles;

    //    var petUpdateResult = await _petService.UpdatePetAsync(petCreateViewModel.Id, petUpdateRequest, cancellationToken);

    //    if (petUpdateResult.IsSuccess is false)
    //    {
    //        TempData["ErrorMessage"] = "Failed to update the pet!";
    //        return RedirectToAction("Update", new { id = petCreateViewModel.Id });
    //    }

    //    TempData["SuccessMessage"] = "Pet updated successfully!";
    //    return RedirectToAction("Details", new { id = petCreateViewModel.Id });
    //}



    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var result = await _petService.GetPetByIdAsync(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound();
        }

        var pet = result.Value;

        // ViewBag.Breeds = new SelectList(await _breedService.GetAllBreedsAsync(cancellationToken), "Id", "Name", pet.BreedId);
        //ViewBag.Cities = new SelectList(await _cityService.GetAllCitiesAsync(cancellationToken), "Id", "Name", pet.CityId);
        //ViewBag.Statuses = Enum.GetValues(typeof(PetStatus)).Cast<PetStatus>().Select(e => new { Id = e, Name = e.ToString() });

        var petCreateRequest = new PetCreateRequest
        {
            Id = pet.Id,
            Name = pet.Name,
            //BreedId = pet.BreedId,
            AgeYears = pet.AgeYears,
            About = pet.About,
            Price = pet.Price,
            //CityId = pet.CityId,
            //Address = pet.Address,
            //PostalCode = pet.PostalCode,
            PostedByUserId = pet.PostedByUserId,
            Status = pet.Status
        };

        return View(petCreateRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PetCreateRequest pet, CancellationToken cancellationToken)
    {
        await _petService.UpdatePetAsync(pet, cancellationToken);
        return RedirectToAction("Profile","Users");
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

}

