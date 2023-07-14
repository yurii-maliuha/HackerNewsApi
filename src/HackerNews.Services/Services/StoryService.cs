using HackerNews.Domain.Model;
using System.Net.Http.Json;
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
        using (var response = await _httpClient.GetAsync(url))
        {
            response.EnsureSuccessStatusCode();

            var bestStoriesIds = await response.Content.ReadFromJsonAsync<IEnumerable<int>>(new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return bestStoriesIds ?? new List<int>();
        }
    }

    public async Task<Story?> GetStory(int storyId)
    {
        var url = $"/v0/item/{storyId}.json";
        using (var response = await _httpClient.GetAsync(url))
        {
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            var story = await response.Content.ReadFromJsonAsync<Story>(new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return story;
        }
    }
}
