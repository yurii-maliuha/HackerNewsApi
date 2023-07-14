using HackerNews.Application.Helpers;
using HackerNews.Application.Mappers;
using HackerNews.Application.Services;
using HackerNews.Domain.DTO;

namespace HackerNews.Application;

public class HackerNewsManager : IHackerNewsManager
{
    private const int API_BATCH_SIZE = 10;

    private readonly SemaphoreSlim _semaphore;
    private readonly IStoryService _storyService;
    private readonly IStoryMapper _storyMapper;
    private readonly IStoryCacheReader _cacheReader;

    public HackerNewsManager(
        IStoryService storyService,
        IStoryMapper storyMapper,
        IStoryCacheReader cacheReader)
    {
        _storyService = storyService;
        _storyMapper = storyMapper;
        _cacheReader = cacheReader;

        _semaphore = new SemaphoreSlim(API_BATCH_SIZE);
    }

    public async Task<IEnumerable<StoryDto>?> GetBestStories(int count)
    {
        var bestStoriesIds = await _cacheReader.GetBestStoriesIds(() => _storyService.GetBestStoriesIds());
        if (bestStoriesIds == null)
        {
            return new List<StoryDto>();
        }

        var tasks = bestStoriesIds.Take(count).Select(async (storyId, index) =>
        {
            await _semaphore.WaitAsync();
            try
            {
                var retrieverTask = index < CachConstants.CACHE_LIMIT
                    ? _cacheReader.GetStory(storyId, () => _storyService.GetStory(storyId))
                    : _storyService.GetStory(storyId);
                return await retrieverTask;
            }
            finally
            {
                _semaphore.Release();
            }
        });

        var stories = await Task.WhenAll(tasks);

        var storiesDtos = stories?.Select(story => _storyMapper.Map(story));
        return storiesDtos;
    }


}
