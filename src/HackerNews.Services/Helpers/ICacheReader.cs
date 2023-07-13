namespace HackerNews.Application.Helpers;

public interface ICacheReader
{
    Task<V?> GetValue<K, V>(K key, Func<Task<V?>> valueRetriever);
}
