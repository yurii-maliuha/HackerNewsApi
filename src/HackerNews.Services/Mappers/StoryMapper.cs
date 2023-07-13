using HackerNews.Domain.DTO;
using HackerNews.Domain.Model;

namespace HackerNews.Services.Mappers
{
    public class StoryMapper : IStoryMapper
    {
        private readonly IDateTimeMapper _dateTimeMapper;
        public StoryMapper(IDateTimeMapper dateTimeMapper)
        {
            _dateTimeMapper = dateTimeMapper;
        }

        public StoryDto Map(Story story)
        {
            return new StoryDto
            (
                postedBy: story.By,
                time: _dateTimeMapper.MapFromUnixStamp(story.Time),
                url: new Uri(story.Url),
                title: story.Title,
                score: story.Score,
                commentCount: story.Descendants
            );
        }
    }
}
