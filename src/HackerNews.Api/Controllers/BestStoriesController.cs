using HackerNews.Domain.DTO;
using HackerNews.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers
{
    [ApiController]
    [Route("best-stories")]
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

        [HttpGet]
        public async Task<IEnumerable<StoryDto>> Get(int count = 3)
        {
            var stories = await _hackerNewsService.GetBestStories(count);
            return stories;

        }
    }
}