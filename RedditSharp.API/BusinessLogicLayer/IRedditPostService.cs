using RedditSharp.API.ViewModel;
using RedditSharp.Things;

namespace RedditSharp.API.BusinessLogicLayer
{
    public interface IRedditPostService
    {
        IEnumerable<PostModel> GetPosts(string subRedditName, int top);
        Task<IEnumerable<PostModel>> GetPostsAsyc(string subRedditName, int top = 5);
        Task<IEnumerable<UserPostModel>> UsersWithMostPostsAsync(string subRedditName, int top = 5);
        Task<int> GetTotalPostCountAsync(string subRedditName);
    }
}
