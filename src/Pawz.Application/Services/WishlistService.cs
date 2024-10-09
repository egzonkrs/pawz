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
    private readonly IPetRepository _petRepository;

    public WishlistService(
        IWishlistRepository wishlistRepository,
        IUnitOfWork unitOfWork,
        ILogger<WishlistService> logger,
        IUserAccessor userAccessor,
        IPetRepository petRepository)
    {
        _wishlistRepository = wishlistRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _userAccessor = userAccessor;
        _petRepository = petRepository;
    }

    public async Task<Result<Wishlist>> GetWishlistForUserAsync()
    {
        try
        {
            var loggedInUser = _userAccessor.GetUserId();

            Console.WriteLine($"Logged in user's id: {loggedInUser}");

            if (string.IsNullOrEmpty(loggedInUser))
            {
                return Result<Wishlist>.Failure(UsersErrors.NotFound(loggedInUser));
            }

            var userWishlist = await _wishlistRepository.GetWishlistForUserAsync(loggedInUser);

            if (userWishlist == null)
            {
                return Result<Wishlist>.Failure("No wishlist found for the user.");
            }

            return Result<Wishlist>.Success(userWishlist);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching wishlist: {ex.Message}");
            return Result<Wishlist>.Failure("An error occurred while fetching the wishlist.");
        }
    }

    public async Task<Result<Wishlist>> AddPetToWishlistAsync(int petId)
    {
        try
        {
            var loggedInUser = _userAccessor.GetUserId();

            if (string.IsNullOrEmpty(loggedInUser))
            {
                return Result<Wishlist>.Failure(UsersErrors.NotFound(loggedInUser));
            }

            var pet = await _petRepository.GetByIdAsync(petId);

            var wishlistEntry = new Wishlist
            {
                UserId = loggedInUser,
                Pets = new List<Pet> { pet },
                IsDeleted = false
            };

            await _wishlistRepository.InsertAsync(wishlistEntry);

            await _unitOfWork.SaveChangesAsync();

            var userWishlist = await _wishlistRepository.GetWishlistForUserAsync(loggedInUser);

            return Result<Wishlist>.Success(userWishlist);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding pet with ID {petId}: {ex.Message}");
            return Result<Wishlist>.Failure($"Error adding pet to wishlist: {ex.Message}");
        }
    }

    public async Task<Result<Wishlist>> RemovePetFromWishlistAsync(int petId)
    {
        try
        {
            var loggedInUser = _userAccessor.GetUserId();

            if (string.IsNullOrEmpty(loggedInUser))
            {
                return Result<Wishlist>.Failure(UsersErrors.NotFound(loggedInUser));
            }

            var wishlistItem = await _wishlistRepository.GetWishlistItemAsync(loggedInUser, petId);

            if (wishlistItem == null)
            {
                return Result<Wishlist>.Failure("The pet is not in the user's wishlist.");
            }

            wishlistItem.IsDeleted = true;
            wishlistItem.DeletedAt = DateTimeOffset.Now;

            await _wishlistRepository.UpdateAsync(wishlistItem);

            await _unitOfWork.SaveChangesAsync();

            var userWishlist = await _wishlistRepository.GetWishlistForUserAsync(loggedInUser);

            return Result<Wishlist>.Success(userWishlist);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error removing pet with ID {petId}: {ex.Message}");
            return Result<Wishlist>.Failure($"Error removing pet from wishlist: {ex.Message}");
        }
    }
}
