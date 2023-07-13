using HackerNews.Domain.DTO;
using HackerNews.Domain.Model;

namespace HackerNews.Services.Mappers
{
    public interface IStoryMapper
    {
        StoryDto Map(Story model);
    }
}
