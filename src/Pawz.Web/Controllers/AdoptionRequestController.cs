using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Domain.Entities;
using Pawz.Web.Extensions;
using Pawz.Web.Models.City;
using Pawz.Web.Models.Pet;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class AdoptionRequestController : Controller
{
    private readonly IAdoptionRequestService _adoptionRequestService;
    private readonly IValidator<AdoptionRequestCreateModel> _validator;
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;
    private readonly IPetService _petservice;
    private readonly IMapper _mapper;

    public AdoptionRequestController(
        IAdoptionRequestService adoptionRequestService,
        IValidator<AdoptionRequestCreateModel> validator,
        ICountryService countryService,
        ICityService cityService,
        IMapper mapper,
        IPetService petservice)
    {
        _adoptionRequestService = adoptionRequestService;
        _validator = validator;
        _countryService = countryService;
        _cityService = cityService;
        _mapper = mapper;
        _petservice = petservice;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var adoptionRequests = await _adoptionRequestService.GetAllAdoptionRequestsAsync(cancellationToken);
        return View(adoptionRequests.Value);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var adoptionRequests = await _adoptionRequestService.GetAdoptionRequestByIdAsync(id, cancellationToken);
        return View(adoptionRequests);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdoptionRequestCreateModel adoptionRequestCreateModel, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(adoptionRequestCreateModel, cancellationToken);

        var countriesResult = await _countryService.GetAllCountriesAsync(cancellationToken);
        var citiesResult = await _cityService.GetAllCitiesAsync(cancellationToken);

        var countriesList = countriesResult.Value ?? new List<Country>();
        var citiesList = citiesResult.Value ?? new List<City>();

        if (validationResult.IsValid is false)
        {
            adoptionRequestCreateModel.Countries = new SelectList(countriesList, "Id", "Name");
            adoptionRequestCreateModel.Cities = new SelectList(citiesList, "Id", "Name");
            adoptionRequestCreateModel.AllCities = citiesList.Select(x => new CityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            }).ToList();

            validationResult.AddErrorsToModelState(ModelState);
            TempData["ErrorMessage"] = "Failed to request for adoption! Please try again!";
            return RedirectToAction("Index", "Home");
        }

        var adoptionRequestCreateRequest = _mapper.Map<AdoptionRequestCreateRequest>(adoptionRequestCreateModel);
        adoptionRequestCreateRequest.PetId = adoptionRequestCreateModel.PetId;
        var adoptionRequestCreateResult = await _adoptionRequestService.CreateAdoptionRequestAsync(adoptionRequestCreateRequest, cancellationToken);

        if (adoptionRequestCreateResult.IsSuccess is false)
        {
            adoptionRequestCreateModel.Countries = new SelectList(countriesList, "Id", "Name");
            adoptionRequestCreateModel.Cities = new SelectList(citiesList, "Id", "Name");
            adoptionRequestCreateModel.AllCities = citiesList.Select(x => new CityViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            }).ToList();

            adoptionRequestCreateResult.AddErrorsToModelState(ModelState);
            TempData["ErrorMessage"] = "Failed to request for adoption! Please try again!";
            return RedirectToAction("Index", "Home");
        }

        if (adoptionRequestCreateResult.IsSuccess)
        {
            TempData["SuccessMessage"] = "Adoption request created successfully!";
        }

        adoptionRequestCreateModel.Countries = new SelectList(countriesList, "Id", "Name");
        adoptionRequestCreateModel.Cities = new SelectList(citiesList, "Id", "Name");

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var adoptionRequest = await _adoptionRequestService.GetAdoptionRequestByIdAsync(id, cancellationToken);
        return View(adoptionRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AdoptionRequest adoptionRequest, CancellationToken cancellationToken)
    {
        await _adoptionRequestService.UpdateAdoptionRequestAsync(adoptionRequest, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var adoptionRequest = await _adoptionRequestService.GetAdoptionRequestByIdAsync(id, cancellationToken);
        return View(adoptionRequest);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        await _adoptionRequestService.DeleteAdoptionRequestAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
