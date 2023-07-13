using HackerNews.Domain.DTO;
using HackerNews.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers
{
    [ApiController]
    [Route("api/best-stories")]
    public class BestStoriesController : ControllerBase
    {
        private readonly ILogger<BestStoriesController> _logger;
        private readonly IHackerNewsService _hackerNewsService;

        public BestStoriesController(
            ILogger<BestStoriesController> logger,
            IHackerNewsService hackerNewsService)
        {
            _logger = logger;
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