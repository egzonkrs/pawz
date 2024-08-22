using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

public class AdoptionRequestController : Controller
{

    private readonly IAdoptionRequestService _adoptionRequestService;

    public AdoptionRequestController(IAdoptionRequestService adoptionRequestService)
    {
        _adoptionRequestService = adoptionRequestService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var adoptionRequests = await _adoptionRequestService.GetAllAdoptionRequestsAsync(cancellationToken);
            return View(adoptionRequests);
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var adoptionRequests = await _adoptionRequestService.GetAdoptionRequestByIdAsync(id, cancellationToken);
            return View(adoptionRequests);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult AdoptionRequest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdoptionRequest adoptionRequest, CancellationToken cancellationToken)
        {
            await _adoptionRequestService.CreateAdoptionRequestAsync(adoptionRequest, cancellationToken);
            return RedirectToAction(nameof(Index));
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


