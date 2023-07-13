using HackerNews.Domain.DTO;
using HackerNews.Domain.Model;

namespace HackerNews.Application.Mappers
{
    public interface IStoryMapper
    {
        StoryDto Map(Story model);
    }
}
