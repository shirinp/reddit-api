using AutoMapper;
using RedditSharp.API.Helper;
using RedditSharp.API.Repository;
using RedditSharp.API.ViewModel;
using RedditSharp.Things;

namespace RedditSharp.API.RedditHelper
{
    public class RedditSharpClient : IRedditSharpClient
    {
        private readonly Reddit _reddit;
        private readonly IMapper _mapper;
        private readonly IRepository<PostModel> _postRepository;
        private readonly ILogger<RedditSharpClient> _logger;
        private readonly IFileWriter _fileWriter;

        public RedditSharpClient(
            Reddit reddit,
            IRepository<PostModel> postRepository,
            ILogger<RedditSharpClient> logger,
            IFileWriter fileWriter,
            IMapper mapper)
        {
            _fileWriter = fileWriter;
            _mapper = mapper;
            _reddit = reddit;
            _logger = logger;
            _postRepository = postRepository;
        }

        private void HanldePost(Post p)
        {
            // Save to Data store
            var post = _mapper.Map<PostModel>(p);
            _postRepository.Save(post);

            // Logging for validation / troubleshooting
            var count = _postRepository.Get().Count();
            _logger.LogInformation($"Adding Post: {count} {post.ToString()}");
            _fileWriter.WriteLine($"Adding Post: {count}    SubRedditName: {p.SubredditName}   Posted on : {p.CreatedUTC}  By: {p.AuthorName}      Votes: {p.Upvotes}     Title: {p.Title}");
        }

        public async Task ReadRedditPostsAsStream(List<string> subReddits)
        {
            List<Task> items = new List<Task>();
            CancellationTokenSource tcs = new CancellationTokenSource();

            foreach (var subReddit in subReddits)
            {
                var sub = await _reddit.GetSubredditAsync(subReddit);
                ListingStream<Post> listingStream = sub.GetPosts(Subreddit.Sort.New, -1).Stream();

                listingStream.Subscribe(p => HanldePost(p), tcs.Token);
                
                items.Add(listingStream.Enumerate(tcs.Token));
            }

            try
            {
                await Task.WhenAll(items).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await _fileWriter.WriteLineAsync($"Task Cancelled.. with an exception : {ex?.Message}");
            }
        }

    }
}
