using HackerNews.Application;
using HackerNews.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers
{
    [ApiController]
    [Route("api/best-stories")]
    public class BestStoriesController : ControllerBase
    {
        private readonly IHackerNewsManager _hackerNewsService;

        public BestStoriesController(
            IHackerNewsManager hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        /// <summary>
        /// Returns N best stories
        /// </summary>
        /// <returns>List of the best stories</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StoryDto>), 200)]
        public async Task<IEnumerable<StoryDto>> Get(int count = 3)
        {
            var stories = await _hackerNewsService.GetBestStories(count);
            return stories;
        }
    }
}