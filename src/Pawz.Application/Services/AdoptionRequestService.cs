using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Pawz.Domain.Common;

namespace Pawz.Application.Services;

public class AdoptionRequestService : IAdoptionRequestService
{
     private readonly IAdoptionRequestRepository _adoptionRequestRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AdoptionService> _logger;

        public AdoptionRequestService(IAdoptionRequestRepository adoptionRequestRepository, IUnitOfWork unitOfWork, ILogger<AdoptionService> logger)
        {
            _adoptionRequestRepository = adoptionRequestRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<bool>> CreateAdoptionRequestAsync(AdoptionRequest adoptionRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started creating an Adoption Request with Id: {AdoptionRequestId} from UserId: {UserId}", adoptionRequest.Id, adoptionRequest.RequesterUserId);

                await _adoptionRequestRepository.InsertAsync(adoptionRequest, cancellationToken);
                var isAdoptionRequestCreated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

                if (isAdoptionRequestCreated)
                {
                    _logger.LogInformation("Created an Adoption Request with Id: {AdoptionRequestId} for UserId: {UserId}", adoptionRequest.Id, adoptionRequest.RequesterUserId);
                    return Result<bool>.Success();
                }

                _logger.LogWarning("Failed to create an Adoption Request with Id: {AdoptionRequestId} from UserId: {UserId}", adoptionRequest.Id, adoptionRequest.RequesterUserId);
                return Result<bool>.Failure(AdoptionRequestErrors.CreationFailed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to create a Adoption Request for the UserId: {UserId}",
                    nameof(AdoptionService), adoptionRequest.RequesterUserId);
                return Result<bool>.Failure(AdoptionRequestErrors.CreationUnexpectedError);
            }
        }

        public async Task<Result<IEnumerable<AdoptionRequest>>> GetAllAdoptionRequestsAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started retrieving all adoption requests.");

                var adoptionsRequests = await _adoptionRequestRepository.GetAllAsync(cancellationToken);

                _logger.LogInformation("Successfully retrieved all adoption requests.");
                return Result<IEnumerable<AdoptionRequest>>.Success(adoptionsRequests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve all adoption requests.",
                                 nameof(AdoptionRequestService));
                return Result<IEnumerable<AdoptionRequest>>.Failure(AdoptionRequestErrors.RetrievalError);
            }
        }

        public async Task<Result<AdoptionRequest>> GetAdoptionRequestByIdAsync(int adoptionRequestId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started retrieving Adoption Requests with Id: {AdoptionRequestId}", adoptionRequestId);

                var adoptionRequest = await _adoptionRequestRepository.GetByIdAsync(adoptionRequestId, cancellationToken);

                if (adoptionRequest is null)
                {
                    _logger.LogWarning("Adoption Request with Id: {AdoptionRequestId} was not found.", adoptionRequestId);
                    return Result<AdoptionRequest>.Failure(AdoptionRequestErrors.NotFound(adoptionRequestId));
                }

                _logger.LogInformation("Successfully retrieved Adoption Request with Id: {AdoptionRequestId}", adoptionRequestId);
                return Result<AdoptionRequest>.Success(adoptionRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to retrieve Adoption Request with Id: {AdoptionRequestId}",
                                 nameof(AdoptionRequestService), adoptionRequestId);
                return Result<AdoptionRequest>.Failure(AdoptionRequestErrors.RetrievalError);
            }
        }

        public async Task<Result<bool>> UpdateAdoptionRequestAsync(AdoptionRequest adoptionRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started updating Adoption Request with Id: {AdoptionRequestId} from UserId: {UserId}", adoptionRequest.Id, adoptionRequest.RequesterUserId);

                await _adoptionRequestRepository.UpdateAsync(adoptionRequest, cancellationToken);
                var adoptionRequestUpdated = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

                if (adoptionRequestUpdated)
                {
                    _logger.LogInformation("Successfully updated Adoption Request with Id: {AdoptionRequestId} from UserId: {UserId}", adoptionRequest.Id, adoptionRequest.RequesterUserId);
                    return Result<bool>.Success(true);
                }
                _logger.LogWarning("Failed to update Adoption Request with Id: {AdoptionRequestId} from UserId: {UserId}. No changes were detected.", adoptionRequest.Id, adoptionRequest.RequesterUserId);
                return Result<bool>.Failure(AdoptionErrors.NoChangesDetected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to update Adoption Request with Id: {AdoptionRequestId} for the UserId: {UserId}",
                    nameof(AdoptionRequestService), adoptionRequest.Id, adoptionRequest.RequesterUserId);
                return Result<bool>.Failure(AdoptionRequestErrors.UpdateUnexpectedError);
            }
        }

        public async Task<Result<bool>> DeleteAdoptionRequestAsync(int adoptionRequestId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Started deleting Adoption Request with Id: {AdoptionRequestId}", adoptionRequestId);

                var adoption = await _adoptionRequestRepository.GetByIdAsync(adoptionRequestId, cancellationToken);
                if (adoption is null)
                {
                    _logger.LogWarning("Adoption Request with Id: {AdoptionRequestId} was not found.", adoptionRequestId);
                    return Result<bool>.Failure(AdoptionRequestErrors.NotFound(adoptionRequestId));
                }

                await _adoptionRequestRepository.DeleteAsync(adoption, cancellationToken);
                var adoptionDeleted = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

                if (adoptionDeleted)
                {
                    _logger.LogInformation("Successfully deleted Adoption Request with Id: {AdoptionRequestId}", adoptionRequestId);
                    return Result<bool>.Success(true);
                }
                _logger.LogWarning("Failed to delete Adoption Request with Id: {AdoptionRequestId}. No changes were detected.", adoptionRequestId);
                return Result<bool>.Failure(AdoptionRequestErrors.NoChangesDetected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the {ServiceName} while attempting to delete Adoption Request with Id: {AdoptionRequestId}",
                                 nameof(AdoptionRequestService), adoptionRequestId);
                return Result<bool>.Failure(AdoptionRequestErrors.DeletionUnexpectedError);
            }
        }
    }

