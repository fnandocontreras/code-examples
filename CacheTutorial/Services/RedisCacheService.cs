using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace CacheTutorial.Services
{
    public class RedisCacheService : ICacheServiceAsync
    {
        private readonly IDistributedCache _cacheService;

        public RedisCacheService(IDistributedCache cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<T> GetAsync<T>(CacheKeySettings keySettings, Func<Task<T>> GetValueFuncAsync, CancellationToken cancellation)
        {
            var stringValue = await _cacheService.GetStringAsync(keySettings.Key, cancellation);
            if (stringValue != null)
                return JsonConvert.DeserializeObject<T>(stringValue);

            T value = await GetValueFuncAsync();
            await SetAsync(keySettings, value, cancellation);

            return value;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellation)
        {
            await _cacheService.RemoveAsync(key, cancellation);
        }

        public async Task SetAsync<T>(CacheKeySettings keySettings, T value, CancellationToken cancellation)
        {
            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = keySettings.Expiry };
            var jsonString = JsonConvert.SerializeObject(value);
            await _cacheService.SetStringAsync(keySettings.Key, jsonString, options);
        }
    }
}
