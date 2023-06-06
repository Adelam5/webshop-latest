using Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace Infrastructure.Services.Cache;
public class CacheService : ICacheService
{
    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();

    private readonly IDistributedCache distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
    }

    public async Task<T?> Get<T>(string key, CancellationToken cancellationToken = default)
        where T : class
    {
        string? cachedValue = await distributedCache.GetStringAsync(
            key,
            cancellationToken);

        if (cachedValue is null)
        {
            return null;
        }

        T? value = JsonConvert.DeserializeObject<T>(cachedValue,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new PrivateResolver()
            });

        return value;
    }

    public async Task<T?> Get<T>(string key, Func<Task<T?>> factory, CancellationToken cancellationToken = default)
        where T : class
    {
        T? cachedValue = await Get<T>(key, cancellationToken);

        if (cachedValue is not null)
        {
            return cachedValue;
        }

        cachedValue = await factory();

        if (cachedValue is not null)
            await Set(key, cachedValue, cancellationToken);

        return cachedValue;
    }

    public async Task Set<T>(string key, T value, CancellationToken cancellationToken = default)
        where T : class
    {
        string cacheValue = JsonConvert.SerializeObject(value);

        await distributedCache.SetStringAsync(key, cacheValue, cancellationToken);

        var sth = await distributedCache.GetStringAsync(key, cancellationToken);

        CacheKeys.TryAdd(key, false);
    }

    public async Task Remove(string key, CancellationToken cancellationToken = default)
    {
        await distributedCache.RemoveAsync(key, cancellationToken);

        CacheKeys.TryRemove(key, out bool _);
    }

    public async Task RemoveByPrefix(string prefixKey, CancellationToken cancellationToken = default)
    {
        IEnumerable<Task> tasks = CacheKeys
            .Keys
            .Where(k => k.StartsWith(prefixKey))
            .Select(k => Remove(k, cancellationToken));

        await Task.WhenAll(tasks);
    }
}

