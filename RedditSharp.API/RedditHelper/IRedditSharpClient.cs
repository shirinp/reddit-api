namespace RedditSharp.API.RedditHelper
{
    public interface IRedditSharpClient
    {
        Task ReadRedditPostsAsStream(List<string> subReddits);
    }
}
