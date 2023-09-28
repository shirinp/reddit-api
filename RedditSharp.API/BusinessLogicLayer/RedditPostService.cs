using AutoMapper;
using RedditSharp.API.Repository;
using RedditSharp.API.ViewModel;
using System.Linq.Expressions;

namespace RedditSharp.API.BusinessLogicLayer
{
    public class RedditPostService : IRedditPostService
    {
        private readonly IRepository<PostModel> _postRepository;
        private readonly ILogger<RedditPostService> _logger;
        private readonly IMapper _mapper;

        public RedditPostService(
            IRepository<PostModel> postRepository,
            ILogger<RedditPostService> logger,
            IMapper mapper) 
        {
            _mapper = mapper;
            _logger = logger;
            _postRepository = postRepository;
        }

        public IEnumerable<PostModel> GetPosts(string subRedditName, int top = 5)
        {
            var result = _postRepository.Get()
                .Where(x => x.SubRedditName == subRedditName)
                .OrderByDescending(item => item.UpVoteCount)
                .Take(top);

            return result;
        }

        public async Task<IEnumerable<PostModel>> GetPostsAsyc(string subRedditName, int top = 5)
        {
            Expression<Func<PostModel, bool>> predicate = x => x.SubRedditName.ToLower() == subRedditName.ToLower();
            
            var p = await _postRepository.GetAsync(predicate);
            return p.OrderByDescending(item => item.UpVoteCount).Take(top);
        }

        public async Task<IEnumerable<UserPostModel>> UsersWithMostPostsAsync(string subRedditName, int top = 5)
        {
            Expression<Func<PostModel, bool>> predicate = x => x.SubRedditName.ToLower() == subRedditName.ToLower();

            var p = await _postRepository.GetAsync(predicate);
            var posts = p
                .GroupBy(x => x.User.UserName)
                .OrderByDescending(item => item.Count())
                .Take(top);

            return _mapper.Map<IEnumerable<UserPostModel>>(posts);
        }

        public async Task<int> GetTotalPostCountAsync(string subRedditName)
        {
            Expression<Func<PostModel, bool>> predicate = x => x.SubRedditName.ToLower() == subRedditName.ToLower();
            return (await _postRepository.GetAsync(predicate)).Count();
        }

    }
}
