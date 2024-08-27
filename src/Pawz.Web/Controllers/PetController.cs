using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Application.Services;
using Pawz.Domain.Entities;
using Pawz.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class PetController : Controller
{
    private readonly IPetService _petService;
    private readonly IUserAccessor _userAccessor;

    public PetController(IPetService petService, IUserAccessor userAccessor)
    {
        _petService = petService;
        _userAccessor = userAccessor;
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Pet pet, CancellationToken cancellationToken)
    {
        await _petService.CreatePetAsync(pet, cancellationToken);
        return RedirectToAction(nameof(Index));
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
        var userId = _userAccessor.GetUserId();
        var result = await _petService.GetPetsByUserIdAsync(userId, cancellationToken);

        if (!result.IsSuccess)
        {
            return View(new List<UserPetViewModel>());
        }
        var pets = result.Value;

        var petViewModels = pets.Select(pet => new UserPetViewModel
        {
            Id = pet.Id,
            Name = pet.Name,
            CreatedAt = pet.CreatedAt,
            Status = pet.Status,
            LocationName = pet.Location?.City,
            PhotoUrl = pet.PetImages.FirstOrDefault()?.ImageUrl,
            BreedName = pet.Breed?.Name
        }).ToList();

        return View(petViewModels);
    }
}
