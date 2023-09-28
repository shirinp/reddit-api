namespace RedditSharp.API.DataStore
{
    public interface IDataStore<TKey, TValue>
    {
        void Add(TKey key, TValue val);
        IEnumerable<TValue> Get(TKey key);

        Task AddAsync(TKey key, TValue val);
        Task<IEnumerable<TValue>> GetAsync(TKey key);
    }
}
