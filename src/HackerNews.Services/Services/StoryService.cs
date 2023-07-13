using HackerNews.Domain.Model;
using System.Text.Json;

namespace HackerNews.Application.Services;

public class StoryService : IStoryService
{
    private readonly HttpClient _httpClient;

    public StoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<int>> GetBestStoriesIds(int count)
    {
        var url = $"/v0/beststories.json?print=pretty&orderBy=%22$key%22&limitToFirst={count}";
        var bestStoriesIdsResponse = await _httpClient.GetStringAsync(url);
        var bestStoriesIds = bestStoriesIdsResponse != null
            ? JsonSerializer.Deserialize<IEnumerable<int>>(bestStoriesIdsResponse)
            : new List<int>();

        return bestStoriesIds;
    }

    public async Task<Story?> GetStory(int storyId)
    {
        Console.WriteLine($"{storyId}: Fetching from API");

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
}
