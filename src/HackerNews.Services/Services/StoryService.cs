using HackerNews.Application.Models;
using HackerNews.Domain.Model;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace HackerNews.Application.Services;

public class StoryService : IStoryService
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions _defaultSerializerOptions =
        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public StoryService(HttpClient httpClient, IOptions<HackerNewsConfiguration> options)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = options.Value.UrlBase;
    }

    public async Task<IEnumerable<int>?> GetBestStoriesIds()
    {
        var url = $"/v0/beststories.json";
        using (var response = await _httpClient.GetAsync(url))
        {
            response.EnsureSuccessStatusCode();

            var bestStoriesIds = await response.Content.ReadFromJsonAsync<IEnumerable<int>>(_defaultSerializerOptions);

            return bestStoriesIds;
        }
    }

    public async Task<Story?> GetStory(int storyId)
    {
        var url = $"/v0/item/{storyId}.json";
        using (var response = await _httpClient.GetAsync(url))
        {
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            var story = await response.Content.ReadFromJsonAsync<Story>(_defaultSerializerOptions);

            return story;
        }
    }
}
