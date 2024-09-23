using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pawz.Application.Interfaces;
using Pawz.Application.Models;
using Pawz.Domain.Entities;
using Pawz.Web.Extensions;
using Pawz.Web.Models;
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

            var errors = validationResult.Errors
           .GroupBy(e => e.PropertyName)
           .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            return Json(new { success = false, errors });
        }

        var adoptionRequestCreateRequest = _mapper.Map<AdoptionRequestCreateRequest>(adoptionRequestCreateModel);
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

            return Json(new { success = false, message = "Failed to create adoption request." });
        }

        adoptionRequestCreateModel.Countries = new SelectList(countriesList, "Id", "Name");
        adoptionRequestCreateModel.Cities = new SelectList(citiesList, "Id", "Name");

        return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
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

    public async Task<IActionResult> GetPetAdoptionRequests(int petId, CancellationToken cancellationToken)
    {
        var adoptionRequestsResult = await _adoptionRequestService.GetAdoptionRequestsByPetIdAsync(petId, cancellationToken);

        if (!adoptionRequestsResult.IsSuccess || adoptionRequestsResult is null || !adoptionRequestsResult.Value.Any())
        {
            return View(new List<AdoptionRequestViewModel>());
        }
        var viewModel = _mapper.Map<List<AdoptionRequestViewModel>>(adoptionRequestsResult.Value);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AcceptAdoptionRequest(int adoptionRequestId, CancellationToken cancellationToken)
    {
        var result = await _adoptionRequestService.AcceptAdoptionRequestAsync(adoptionRequestId, cancellationToken);

        if (result.IsSuccess)
        {
            TempData["SuccessMessage"] = "Adoption request successfully accepted.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to accept the adoption request.";
        }
        return Redirect(Request.Headers["Referer"].ToString());
    }


    [HttpPost]
    public async Task<IActionResult> RejectAdoptionRequest(int adoptionRequestId, CancellationToken cancellationToken)
    {
        var result = await _adoptionRequestService.RejectAdoptionRequestAsync(adoptionRequestId, cancellationToken);

        if (result.IsSuccess)
        {
            TempData["SuccessMessage"] = "Adoption request successfully rejected.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to reject the adoption request.";
        }
        return Redirect(Request.Headers["Referer"].ToString());
    }

}
