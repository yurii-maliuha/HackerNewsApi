using HackerNews.Domain.DTO;

namespace HackerNews.Services;

public interface IHackerNewsService
{
    Task<IEnumerable<StoryDto>?> GetBestStories(int count);
}
