using HackerNews.Domain.Model;

namespace HackerNews.Application.Helpers;

public interface IStoryCacheReader
{
    ValueTask<Story?> GetValue(int key, Func<Task<Story?>> valueRetriever);
}
