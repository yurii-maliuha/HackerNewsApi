using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.Application.Helpers;

public class CacheReader : ICacheReader
{
    private readonly IMemoryCache _memoryCache;
    public CacheReader(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<V?> GetValue<K, V>(K key, Func<Task<V?>> valueRetriever)
    {
        _memoryCache.TryGetValue(key, out V cacheValue);
        if (cacheValue != null)
        {
            return cacheValue;
        }

        var value = await valueRetriever();

        if (value != null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        return value;
    }
}
