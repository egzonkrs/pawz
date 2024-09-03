using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Application.Models.Pet;
using Pawz.Application.Models.PetModels;
using Pawz.Web.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class PetController : Controller
{
    private readonly IPetService _petService;
    private readonly IUserAccessor _userAccessor;
    private readonly IMapper _mapper;

    public PetController(IPetService petService, IUserAccessor userAccessor, IMapper mapper)
    {
        _petService = petService;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var result = await _petService.GetAllPetsWithRelatedEntities(cancellationToken);
        return View(result);
    }

    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var result = await _petService.GetPetByIdAsync(id, cancellationToken);
        return View(result);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PetViewModel petViewModel, CancellationToken cancellationToken)
    {
        await _petService.CreatePetAsync(_mapper.Map<PetRequest>(petViewModel), cancellationToken);
        return RedirectToAction(nameof(Index));
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
        await _petService.UpdatePetAsync(id, _mapper.Map<PetRequest>(petViewModel), cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _petService.GetPetByIdAsync(id, cancellationToken);
        var viewModel = _mapper.Map<PetViewModel>(result.Value);
        return View(viewModel);
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
