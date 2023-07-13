using HackerNews.Domain.DTO;

namespace HackerNews.Application.Services;

public interface IHackerNewsService
{
    Task<IEnumerable<StoryDto>?> GetBestStories(int count);
}
