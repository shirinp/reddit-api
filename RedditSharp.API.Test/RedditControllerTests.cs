using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RedditSharp.API.BusinessLogicLayer;
using RedditSharp.API.Controllers;
using RedditSharp.API.RedditHelper;
using RedditSharp.API.Test.MockData;

namespace RedditSharp.API.Test
{
    public class RedditControllerTests
    {
        private Mock<Microsoft.Extensions.Configuration.IConfiguration> mockConfiguration;
        private Mock<IRedditSharpClient> mockRedditSharpClient;
        private Mock<IRedditPostService> redditService;

        public RedditControllerTests()
        {
            mockConfiguration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            mockRedditSharpClient = new Mock<IRedditSharpClient>();
            redditService = new Mock<IRedditPostService>();
        }

        [Fact]
        public async Task GetPostsAsyncTestStatus200()
        {
            redditService.Setup(_ => _.GetPostsAsyc("funny", 5)).ReturnsAsync(RedditClientMockData.GetPosts(5));
            var setup = new RedditController(redditService.Object, mockRedditSharpClient.Object, mockConfiguration.Object);
            var result = (OkObjectResult)await setup.GetPostsByUpvoteAsync("funny");
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetPostsAsyncTestStatus400()
        {
            var setup = new RedditController(redditService.Object, mockRedditSharpClient.Object, mockConfiguration.Object);
            var result = (BadRequestResult)await setup.GetPostsByUpvoteAsync(string.Empty);
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetUsersAsyncTestStatus200()
        {
            redditService.Setup(_ => _.UsersWithMostPostsAsync("funny", 5)).ReturnsAsync(RedditClientMockData.GetUsers(5));
            var setup = new RedditController(redditService.Object, mockRedditSharpClient.Object, mockConfiguration.Object);
            var result = (OkObjectResult)(await setup.UsersWithMostPosts("funny", 5)).Result;
            result?.StatusCode.Should().Be(200);
            result?.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetUsersAsyncTestStatus400()
        {
            var setup = new RedditController(redditService.Object, mockRedditSharpClient.Object, mockConfiguration.Object);
            var result = (BadRequestResult)(await setup.UsersWithMostPosts(string.Empty)).Result;
            result?.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetTotalPostAsyncTestStatus200()
        {
            redditService.Setup(_ => _.GetPostsAsyc("funny", 5)).ReturnsAsync(RedditClientMockData.GetPosts(5));
            var setup = new RedditController(redditService.Object, mockRedditSharpClient.Object, mockConfiguration.Object);
            var result = (OkObjectResult)(await setup.GetTotalPostCountAsync("funny")).Result;
            result?.StatusCode.Should().Be(200);

            int count = (int)result.Value;
            count.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task GetTotalPostAsyncTestStatus400()
        {
            var setup = new RedditController(redditService.Object, mockRedditSharpClient.Object, mockConfiguration.Object);
            var result = (BadRequestResult)(await setup.GetTotalPostCountAsync(string.Empty)).Result;
            result?.StatusCode.Should().Be(400);
        }
    }
}
