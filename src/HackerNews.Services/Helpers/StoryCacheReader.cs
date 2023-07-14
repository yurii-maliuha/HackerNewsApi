using HackerNews.Domain.Model;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.Application.Helpers;

public class StoryCacheReader : CacheBase, IStoryCacheReader
{
    private const int MIN_HIGH_PRIORITY_CACHABLE_ITEM = 340;
    private const int MIN_NORMAL_PRIORITY_CACHABLE_ITEM = 200;

    public StoryCacheReader(IMemoryCache memoryCache)
        : base(memoryCache)
    {
    }

    public async Task<IEnumerable<int>?> GetBestStoriesIds(Func<Task<IEnumerable<int>?>> valueRetriever)
    {
        var optionsBuilder = (IEnumerable<int> ids) => new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetPriority(CacheItemPriority.NeverRemove);

        return await base.GetValue(0, valueRetriever, optionsBuilder);
    }

    public async Task<Story?> GetStory(int storyId, Func<Task<Story?>> valueRetriever)
    {
        var optionsBuilder = (Story story) => new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetPriority(ResolvePriority(story.Score))
                .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        return await base.GetValue(storyId, valueRetriever, optionsBuilder);
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
