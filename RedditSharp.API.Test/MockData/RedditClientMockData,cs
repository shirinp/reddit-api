using RedditSharp.API.DataStore;
using RedditSharp.API.ViewModel;

namespace RedditSharp.API.Test.MockData
{
    public class RedditClientMockData
    {
        public static string EntityName = "Reddit.Post";
        public static List<PostModel> GetPosts(int items) {

            return Enumerable.Range(1, items).Select(item => new PostModel() {
                Id = $"Id:{item}",
                SubRedditName = item % 2 == 0 ? "funny" : "askreddit",
                Title = $"Funny Post: {item}",
                UpVoteCount = item * 1,
                User = new UserModel() { UserName = $"user{ item }"},
                PostedAtUtc = DateTime.UtcNow.AddMinutes(item),
            }).ToList();
        }

        public static List<UserPostModel> GetUsers(int items)
        {
            return Enumerable.Range(1, items).Select(it => new UserPostModel()
            {
                User = new UserModel() { UserName = $"user{it}" },
                Posts = GetPosts(it),
            }).ToList();
        }

        public static List<PostModel> GetPostsForUsersQuery(int items)
        {

            return Enumerable.Range(1, items).Select(item => new PostModel()
            {
                Id = $"Id:{item}",
                SubRedditName = item % 2 == 0 ? "funny" : "askreddit",
                Title = $"Funny Post: {item}",
                UpVoteCount = item * 1,
                User = new UserModel() { UserName =  $"user{item % 10}" },
                PostedAtUtc = DateTime.UtcNow.AddMinutes(item),
            }).ToList();
        }

        public static InMemoryDataStore<string, PostModel> GetPostDataStore(int items)
        {
            var dict = new InMemoryDataStore<string, PostModel>();
            foreach(var item in GetPostsForUsersQuery(items))
            {
                dict.Add(EntityName, item);
            }

            return dict;
        }
    }
}
