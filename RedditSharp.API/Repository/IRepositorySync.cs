using System.Linq.Expressions;

namespace RedditSharp.API.Repository
{
    public interface IRepositorySync<T>
    {
        IEnumerable<T> Get();
        void Save(T entity);
        IEnumerable<T> Get(Expression<Func<T, bool>> searchExpression);
    }
}
