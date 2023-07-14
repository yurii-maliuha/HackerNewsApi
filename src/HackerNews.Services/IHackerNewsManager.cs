using HackerNews.Domain.DTO;

namespace HackerNews.Application;

public interface IHackerNewsManager
{
    Task<IEnumerable<StoryDto>?> GetBestStories(int count);
}
