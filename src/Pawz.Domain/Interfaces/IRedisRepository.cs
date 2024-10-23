using System;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

public interface IRedisRepository
{
    Task<bool> SetDataAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task<T?> GetDataAsync<T>(string key);
    Task<bool> DeleteDataAsync(string key);
    Task<bool> HasKeyAsync(string pattern);
}
