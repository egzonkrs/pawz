using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<WishlistService> _logger;
    private readonly IUserAccessor _userAccessor;

    public WishlistService(IWishlistRepository wishlistRepository, IUnitOfWork unitOfWork, ILogger<WishlistService> logger, IUserAccessor userAccessor)
    {
        _wishlistRepository = wishlistRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _userAccessor = userAccessor;
    }

    public async Task<Result<List<Wishlist>>> AddPetToWishlistAsync(string userId, int petId)
    {
        try
        {
            userId = _userAccessor.GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Result<List<Wishlist>>.Failure(UsersErrors.RetrievalError);
            }

            var wishlistEntry = new Wishlist
            {
                UserId = userId,
                PetId = petId,
                IsDeleted = false
            };

            await _wishlistRepository.InsertAsync(wishlistEntry);

            await _unitOfWork.SaveChangesAsync();

            var userWishlist = await _wishlistRepository.GetWishlistForUserAsync(userId);

            return Result<List<Wishlist>>.Success(userWishlist.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding pet with ID {petId} to wishlist for user {userId}: {ex.Message}");
            return Result<List<Wishlist>>.Failure($"Error adding pet to wishlist: {ex.Message}");
        }
    }

    public async Task<Result<List<Wishlist>>> RemovePetFromWishlistAsync(string userId, int petId)
    {
        try
        {
            userId = _userAccessor.GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Result<List<Wishlist>>.Failure(UsersErrors.RetrievalError);
            }

            var wishlistItem = await _wishlistRepository.GetWishlistItemAsync(userId, petId);

            if (wishlistItem == null)
            {
                return Result<List<Wishlist>>.Failure("The pet is not in the user's wishlist.");
            }

            wishlistItem.IsDeleted = true;
            wishlistItem.DeletedAt = DateTimeOffset.Now;

            await _wishlistRepository.UpdateAsync(wishlistItem);

            await _unitOfWork.SaveChangesAsync();

            var userWishlist = await _wishlistRepository.GetWishlistForUserAsync(userId);

            return Result<List<Wishlist>>.Success(userWishlist.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error removing pet with ID {petId} from wishlist for user {userId}: {ex.Message}");
            return Result<List<Wishlist>>.Failure($"Error removing pet from wishlist: {ex.Message}");
        }
    }

}
