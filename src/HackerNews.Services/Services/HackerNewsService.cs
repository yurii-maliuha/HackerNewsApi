using HackerNews.Application.Helpers;
using HackerNews.Application.Mappers;
using HackerNews.Domain.DTO;
using HackerNews.Domain.Model;

namespace HackerNews.Application.Services;

public class HackerNewsService : IHackerNewsService
{
    private const int API_BATCH_SIZE = 10;

    private readonly SemaphoreSlim _semaphore;
    private readonly IStoryService _storyService;
    private readonly IStoryMapper _storyMapper;
    private readonly ICacheReader _cacheReader;

    public HackerNewsService(
        IStoryService storyService,
        IStoryMapper storyMapper,
        ICacheReader cacheReader)
    {
        _storyService = storyService;
        _storyMapper = storyMapper;
        _cacheReader = cacheReader;

        _semaphore = new SemaphoreSlim(API_BATCH_SIZE);
    }

    public async Task<IEnumerable<StoryDto>?> GetBestStories(int count)
    {
        var bestStoriesIds = await _storyService.GetBestStoriesIds(count);

        var tasks = bestStoriesIds.Select(async storyId =>
        {
            Console.WriteLine($"{storyId} story is queued");
            await _semaphore.WaitAsync();
            try
            {
                Console.WriteLine($"{storyId} story entered");
                return await _cacheReader.GetValue<int, Story>(storyId, () => _storyService.GetStory(storyId));
            }
            finally
            {
                Console.WriteLine($"{storyId} story exited");
                _semaphore.Release();
            }
        });

        var stories = await Task.WhenAll(tasks);

        var storiesDtos = stories?.Select(story => _storyMapper.Map(story));
        return storiesDtos;
    }


}
