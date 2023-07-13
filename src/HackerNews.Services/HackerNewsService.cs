using HackerNews.Domain.DTO;
using HackerNews.Domain.Model;
using HackerNews.Services.Mappers;
using System.Text.Json;

namespace HackerNews.Services;

public class HackerNewsService : IHackerNewsService
{
    private const int API_BATCH_SIZE = 1;

    private readonly HttpClient _httpClient;
    private readonly SemaphoreSlim _semaphore;
    private readonly IStoryMapper _storyMapper;

    public HackerNewsService(
        HttpClient httpClient,
        IStoryMapper storyMapper)
    {
        _httpClient = httpClient;
        _semaphore = new SemaphoreSlim(API_BATCH_SIZE);
        _storyMapper = storyMapper;
    }

    public async Task<IEnumerable<StoryDto>?> GetBestStories(int count)
    {
        var url = $"/v0/beststories.json?print=pretty&orderBy=%22$key%22&limitToFirst={count}";
        var bestStoriesIdsResponse = await _httpClient.GetStringAsync(url);
        var bestStoriesIds = bestStoriesIdsResponse != null
            ? JsonSerializer.Deserialize<IEnumerable<int>>(bestStoriesIdsResponse)
            : new List<int>();


        var tasks = bestStoriesIds.Select(storyId => GetStory(storyId));
        var stories = await Task.WhenAll(tasks);

        var storiesDtos = stories?.Select(s => _storyMapper.Map(s));
        return storiesDtos;
    }

    public async Task<Story?> GetStory(int storyId)
    {
        await _semaphore.WaitAsync();
        try
        {
            var url = $"/v0/item/{storyId}.json";
            var response = await _httpClient.GetStringAsync(url);
            if (response != null)
            {
                return JsonSerializer.Deserialize<Story>(response, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }

            return null;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
