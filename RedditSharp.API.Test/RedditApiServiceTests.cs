using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RedditSharp.API.BusinessLogicLayer;
using RedditSharp.API.Mapping;
using RedditSharp.API.Repository;
using RedditSharp.API.Test.MockData;
using RedditSharp.API.ViewModel;

namespace RedditSharp.API.Test
{
    public class RedditApiServiceTests
    {
        private Mock<ILogger<RedditPostService>> loggerMockService;
        private static IMapper? _mapper = null;
        private RedditPostService setup;

        public RedditApiServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new PostProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            loggerMockService = new Mock<ILogger<RedditPostService>>();

            var repo = new Repository<PostModel>(RedditClientMockData.GetPostDataStore(100));
            setup = new RedditPostService(repo, loggerMockService.Object, _mapper);
        }

        [Fact]
        public async Task RedditService_GetPostAsyncTest()
        {
            var result = await setup.GetPostsAsyc("funny", 5);
            result.Count().Should().BeGreaterThanOrEqualTo(5);

            var result1 = await setup.GetPostsAsyc("askReddit", 5);
            result1.Count().Should().BeGreaterThanOrEqualTo(5);

            var result2 = await setup.GetPostsAsyc("test", 5);
            result2.Count().Should().Be(0);
        }

        [Fact]
        public async Task RedditService_GetUsersByMostPostsAsyncTest()
        {
            var result = await setup.UsersWithMostPostsAsync("funny", 5);
            result.Count().Should().BeGreaterThanOrEqualTo(5);

            var result1 = await setup.UsersWithMostPostsAsync("askReddit", 5);
            result1.Count().Should().BeGreaterThanOrEqualTo(5);

            var result2 = await setup.UsersWithMostPostsAsync("test", 5);
            result2.Count().Should().Be(0);
        }

        [Fact]
        public async Task RedditService_GetTotalCountAsyncTest()
        {
            var result = await setup.GetTotalPostCountAsync("funny");
            result.Should().BeGreaterThanOrEqualTo(5);

            var result1 = await setup.GetTotalPostCountAsync("askReddit");
            result1.Should().BeGreaterThanOrEqualTo(5);

            var result2 = await setup.GetTotalPostCountAsync("test");
            result2.Should().Be(0);
        }
    }
}
