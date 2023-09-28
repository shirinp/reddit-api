namespace RedditSharp.API.ViewModel
{
    public class PostModel
    {
        public string? SubRedditName { get; set; }
        public string? Id { get; set; }
        public string? Title { get; set; }
        public DateTime PostedAtUtc { get; set; }
        public int UpVoteCount { get; set; }
        public UserModel? User { get; set; }

        public override string ToString()
        {
            return $"SubRedditName: {SubRedditName}   Posted on : {PostedAtUtc}  By: {User?.UserName}      Votes: {UpVoteCount}     Title: {Title}";
        }
    }
}
