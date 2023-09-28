using RedditSharp;
using RedditSharp.API.DataStore;
using RedditSharp.API.ViewModel;
using RedditSharp.Things;
using System.Linq.Expressions;

namespace RedditSharp.API.Repository
{
    public class Repository<PostModel> : IRepository<PostModel>
    {
        private readonly string EntityName = "Reddit.Post";
        private readonly IDataStore<string, PostModel>  _dataStore;

        public Repository(IDataStore<string, PostModel> dataStore) 
        {
            _dataStore = dataStore;
        }

        public IEnumerable<PostModel> Get()
        {
            return _dataStore.Get(EntityName);
        }

        public async Task<IEnumerable<PostModel>> GetAsync()
        {
            return await _dataStore.GetAsync(EntityName);
        }

        public void Save(PostModel entity)
        {
            _dataStore.Add(EntityName, entity);
        }

        public async Task SaveAsync(PostModel entity)
        {
            await _dataStore.AddAsync(EntityName, entity);
        }

        public IEnumerable<PostModel> Get(Expression<Func<PostModel, bool>> searchExpression)
        {
            return _dataStore.Get(EntityName).AsQueryable().Where(searchExpression);
        }

        public async Task<IEnumerable<PostModel>> GetAsync(Expression<Func<PostModel, bool>> searchExpression)
        {
            return (await _dataStore.GetAsync(EntityName)).AsQueryable().Where(searchExpression);
        }
    }
}
