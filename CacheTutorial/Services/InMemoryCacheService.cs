using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CacheTutorial.Services
{
    public class InMemoryCacheService : ICacheServiceAsync
    {
        private readonly IMemoryCache _cache;
        public InMemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetAsync<T>(CacheKeySettings keySettings, Func<Task<T>> GetValueFuncAsync, CancellationToken cancellation = default)
        {
            if (_cache.TryGetValue<T>(keySettings.Key, out T cachedValue))
                return cachedValue;

            T value = await GetValueFuncAsync();
            await SetAsync(keySettings, value);

            return value;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellation = default)
        {
            _cache.Remove(key);
        }

        public async Task SetAsync<T>(CacheKeySettings keySettings, T value, CancellationToken cancellation = default)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(keySettings.Expiry);

            _cache.Set<T>(keySettings.Key, value, cacheEntryOptions);
        }
    }
}
