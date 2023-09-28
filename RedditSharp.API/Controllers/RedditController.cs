using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditSharp.API.BusinessLogicLayer;
using RedditSharp.API.RedditHelper;
using RedditSharp.API.ViewModel;

namespace RedditSharp.API.Controllers
{
    
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class RedditController : ControllerBase
    {
        private readonly IRedditPostService _redditPostService;
        private readonly IRedditSharpClient _redditSharpClient;
        public RedditController(
            IRedditPostService redditPostService, 
            IRedditSharpClient redditSharpClient) {

            _redditPostService = redditPostService;
            _redditSharpClient = redditSharpClient;
        }

        [HttpPost("/ReadRedditPostsAsStream")]
        public async Task<ActionResult<bool>> ReadRedditPostsAsStream(List<string> subReddits)
        {
            _ = Task.Run(() => _redditSharpClient.ReadRedditPostsAsStream(subReddits));
            return await Task.FromResult(true);
        }

        [HttpGet("posts/{subRedditName}")]
        public async Task<IActionResult> GetPostsByUpvoteAsync(string subRedditName, int top = 5)
        {
            if(string.IsNullOrEmpty(subRedditName))
            {
                return BadRequest();
            }

            var result = await _redditPostService.GetPostsAsyc(subRedditName, top);
            return Ok(result);
        }

        [HttpGet("users/{subRedditName}")]
        public async Task<ActionResult<UserModel>> UsersWithMostPosts(string subRedditName, int top = 5)
        {
            if (string.IsNullOrEmpty(subRedditName))
            {
                return BadRequest();
            }

            var result = await _redditPostService.UsersWithMostPostsAsync(subRedditName, top);
            return Ok(result);
        }

        [HttpGet("posts/total/{subRedditName}")]
        public async Task<ActionResult<int>> GetTotalPostCountAsync(string subRedditName)
        {
            if (string.IsNullOrEmpty(subRedditName))
            {
                return BadRequest();
            }

            return Ok(await _redditPostService.GetTotalPostCountAsync(subRedditName));
        }
    }
}
