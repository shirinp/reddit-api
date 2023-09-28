namespace RedditSharp.API.ViewModel
{
    public class UserPostModel
    {
        public UserPostModel()
        {
            Posts = new List<PostModel>();
        }
        public UserModel User { get; set; }
        public int PostCount => Posts.Count(); 
        public IEnumerable<PostModel> Posts { get; set; }
    }
}
