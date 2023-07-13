using HackerNews.Domain.Model;

namespace HackerNews.Application.Services
{
    public interface IStoryService
    {
        Task<IEnumerable<int>> GetBestStoriesIds(int count);

        Task<Story?> GetStory(int storyId);
    }
}
