namespace RedditSharp.API.Repository
{
    public interface IRepository<T> : IRepositorySync<T>, IRepositoryAsync<T>
    {
    }
}
