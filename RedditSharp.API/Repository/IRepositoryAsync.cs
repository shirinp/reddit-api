using System.Linq.Expressions;

namespace RedditSharp.API.Repository
{
    public interface IRepositoryAsync<T>
    {
        Task SaveAsync(T entity);
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> searchExpression);
    }
}
