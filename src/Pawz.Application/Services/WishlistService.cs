using Pawz.Application.Interfaces;
using Pawz.Domain.Entities;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

public class WishlistService(IConnectionMultiplexer redis) : IWishlistService
{
    private readonly IDatabase _database = redis.GetDatabase();

    public async Task<Wishlist> GetWishlistAsync(string key)
    {
        var data = await _database.StringGetAsync(key);

        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Wishlist>(data!);
    }

    public async Task<Wishlist> SetWishlistAsync(Wishlist wishlist)
    {
        var created = await _database.StringSetAsync(wishlist.Id,
            JsonSerializer.Serialize(wishlist), TimeSpan.FromDays(1));

        if (!created) return null;
        return await GetWishlistAsync(wishlist.Id);
    }

    public async Task<bool> DeleteWishlistAsync(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }

    public async Task<bool> TestRedisConnectionAsync()
    {
        try
        {
            var ping = await _database.PingAsync();  // Ping the Redis server to test connection
            return ping.TotalMilliseconds >= 0;  // If ping succeeds, return true
        }
        catch (Exception)
        {
            return false;  // Return false if there's an error or exception during ping
        }
    }

}
