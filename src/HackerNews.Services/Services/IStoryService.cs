using HackerNews.Domain.Model;

namespace HackerNews.Application.Services;

public interface IStoryService
{
    Task<IEnumerable<int>?> GetBestStoriesIds();

    Task<Story?> GetStory(int storyId);
}
