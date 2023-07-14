using HackerNews.Domain.Model;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.Application.Helpers;

public class StoryCacheReader : IStoryCacheReader
{
    private const int MIN_HIGH_PRIORITY_CACHABLE_ITEM = 340;
    private const int MIN_NORMAL_PRIORITY_CACHABLE_ITEM = 200;
    private const int MIN_LOW_PRIORITY_CACHABLE_ITEM = 160;

    private readonly IMemoryCache _memoryCache;
    public StoryCacheReader(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async ValueTask<Story?> GetValue(int key, Func<Task<Story?>> valueRetriever)
    {
        _memoryCache.TryGetValue(key, out Story cacheValue);
        if (cacheValue != null)
        {
            return cacheValue;
        }

        var value = await valueRetriever();
        if (value != null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetPriority(ResolvePriority(value.Score))
                .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        return value;
    }

    private CacheItemPriority ResolvePriority(int score)
    {
        var itemPrioroty = score > MIN_NORMAL_PRIORITY_CACHABLE_ITEM
            ? CacheItemPriority.Normal : CacheItemPriority.Low;

        itemPrioroty = score > MIN_HIGH_PRIORITY_CACHABLE_ITEM
            ? CacheItemPriority.High : itemPrioroty;

        return itemPrioroty;
    }
}
