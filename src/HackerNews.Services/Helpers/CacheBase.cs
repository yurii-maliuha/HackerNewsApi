using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.Application.Helpers;

public abstract class CacheBase
{
    private readonly IMemoryCache _memoryCache;
    public CacheBase(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<V?> GetValue<K, V>(K key, Func<Task<V>> valueRetriever, Func<V, MemoryCacheEntryOptions> optionsBuilder)
    {
        _memoryCache.TryGetValue(key, out V cacheValue);
        if (cacheValue != null)
        {
            return cacheValue;
        }

        var value = await valueRetriever();
        if (value != null)
        {
            _memoryCache.Set(key, value, optionsBuilder(value));
        }

        return value;
    }
}
