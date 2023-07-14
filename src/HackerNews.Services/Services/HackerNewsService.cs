using HackerNews.Application.Helpers;
using HackerNews.Application.Mappers;
using HackerNews.Domain.DTO;

namespace HackerNews.Application.Services;

public class HackerNewsService : IHackerNewsService
{
    private const int API_BATCH_SIZE = 10;

    private readonly SemaphoreSlim _semaphore;
    private readonly IStoryService _storyService;
    private readonly IStoryMapper _storyMapper;
    private readonly IStoryCacheReader _cacheReader;

    public HackerNewsService(
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
        var bestStoriesIds = await _storyService.GetBestStoriesIds(count);

        var tasks = bestStoriesIds.Select(async (storyId, index) =>
        {
            await _semaphore.WaitAsync();
            try
            {
                if (index < 90)
                {
                    return await _cacheReader.GetValue(storyId, () => _storyService.GetStory(storyId));
                }

                return await _storyService.GetStory(storyId);
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
