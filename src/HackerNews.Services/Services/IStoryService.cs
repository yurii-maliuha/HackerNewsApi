using HackerNews.Domain.DTO;

namespace HackerNews.Application.Services
{
    public interface IStoryService
    {
        Task<IEnumerable<StoryDto>?> GetBestStories(int count);
    }
}
