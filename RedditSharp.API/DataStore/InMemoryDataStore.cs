using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Collections.Concurrent;


namespace RedditSharp.API.DataStore
{
    public class InMemoryDataStore<TKey, TValue> : IDataStore<TKey, TValue> where TKey : notnull
    {
        private readonly ConcurrentDictionary<TKey, List<TValue>> _dictionary;

        public InMemoryDataStore() {
            _dictionary = new ConcurrentDictionary<TKey, List<TValue>>();
        }

        public void Add(TKey key, TValue val)
        {
            if (!_dictionary.ContainsKey(key))
            {
                if(_dictionary.TryAdd(key, new List<TValue>()))
                {
                    _dictionary[key].Add(val);
                }
            }
            else
            {
                _dictionary[key].Add(val);
            }
        }

        public async Task AddAsync(TKey key, TValue val) 
        {
            await Task.Run(() => 
            {
                if (!_dictionary.ContainsKey(key))
                {
                    if (_dictionary.TryAdd(key, new List<TValue>()))
                    {
                        _dictionary[key].Add(val);
                    }
                }
                else
                {
                    _dictionary[key].Add(val);
                }
            });
        }

        public async Task<IEnumerable<TValue>> GetAsync(TKey key) 
        {
            if (_dictionary.TryGetValue(key, out var val))
            {
                return await Task.FromResult(val);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public IEnumerable<TValue> Get(TKey key)
        {
            if(_dictionary.TryGetValue(key, out var val))
            {
                return val;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
