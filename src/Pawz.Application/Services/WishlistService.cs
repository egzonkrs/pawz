using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<WishlistService> _logger;
    private readonly IUserAccessor _userAccessor;
    private readonly IPetRepository _petRepository;
    private readonly IDatabase _database;

    public WishlistService(
        IWishlistRepository wishlistRepository,
        IUnitOfWork unitOfWork,
        ILogger<WishlistService> logger,
        IUserAccessor userAccessor,
        IPetRepository petRepository,
        IConnectionMultiplexer redis)
    {
        _wishlistRepository = wishlistRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _userAccessor = userAccessor;
        _petRepository = petRepository;
        _database = redis.GetDatabase();
    }

    public async Task<Result<Wishlist>> GetWishlistForUserAsync()
    {
        try
        {
            var loggedInUser = _userAccessor.GetUserId();
            if (string.IsNullOrEmpty(loggedInUser))
            {
                return Result<Wishlist>.Failure(UsersErrors.NotFound(loggedInUser));
            }

            Wishlist? userWishlist = await GetWishlistAsync(loggedInUser);
            if (userWishlist != null)
            {
                return Result<Wishlist>.Success(userWishlist);
            }

            userWishlist = await _wishlistRepository.GetWishlistForUserAsync(loggedInUser);
            if (userWishlist == null)
            {
                return Result<Wishlist>.Failure("No wishlist found for the user.");
            }

            // Save to Redis for future use
            await SetWishlistAsync(userWishlist);

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

            var userWishlist = await _wishlistRepository.GetWishlistForUserAsync(loggedInUser);

            if (userWishlist == null)
            {
                userWishlist = new Wishlist
                {
                    UserId = loggedInUser,
                    Pets = new List<Pet> { pet },
                    IsDeleted = false
                };

                await _wishlistRepository.InsertAsync(userWishlist);
            }

            userWishlist.Pets.Add(pet);
            await _wishlistRepository.UpdateAsync(userWishlist);

            await _unitOfWork.SaveChangesAsync();

            await SetWishlistAsync(userWishlist);

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

            var userWishlist = await _wishlistRepository.GetWishlistForUserAsync(loggedInUser);
            if (userWishlist == null || !userWishlist.Pets.Exists(p => p.Id == petId))
            {
                return Result<Wishlist>.Failure("The pet is not in the user's wishlist.");
            }

            userWishlist.Pets.RemoveAll(p => p.Id == petId);

            await _wishlistRepository.UpdateAsync(userWishlist);
            await _unitOfWork.SaveChangesAsync();

            await SetWishlistAsync(userWishlist);

            return Result<Wishlist>.Success(userWishlist);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error removing pet with ID {petId}: {ex.Message}");
            return Result<Wishlist>.Failure($"Error removing pet from wishlist: {ex.Message}");
        }
    }

    public async Task<Wishlist> SetWishlistAsync(Wishlist wishlist)
    {
        try
        {
            var created = await _database.StringSetAsync(wishlist.Id.ToString(),
                JsonSerializer.Serialize(wishlist), TimeSpan.FromDays(30));
            return created ? wishlist : null;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Redis failed to set wishlist. Error: {ex.Message}");
            return null;
        }
    }

    public async Task<Wishlist> GetWishlistAsync(string key)
    {
        try
        {
            var data = await _database.StringGetAsync(key);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Wishlist>(data);
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Redis failed to get wishlist. Error: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeleteWishlistAsync(string key)
    {
        try
        {
            await _database.KeyDeleteAsync(key);

            var wishlist = await _wishlistRepository.GetWishlistForUserAsync(key);
            if (wishlist != null)
            {
                wishlist.IsDeleted = true;
                wishlist.DeletedAt = DateTimeOffset.Now;

                await _wishlistRepository.UpdateAsync(wishlist);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Failed to delete wishlist from Redis. Error: {ex.Message}");
            return false;
        }
    }
}
