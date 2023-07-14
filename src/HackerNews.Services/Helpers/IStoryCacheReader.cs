using HackerNews.Domain.Model;

namespace HackerNews.Application.Helpers;

public interface IStoryCacheReader
{
    Task<IEnumerable<int>?> GetBestStoriesIds(Func<Task<IEnumerable<int>?>> valueRetriever);
    Task<Story?> GetStory(int storyId, Func<Task<Story?>> valueRetriever);
}
