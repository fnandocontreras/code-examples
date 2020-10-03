using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CacheTutorial.Services
{
    public interface ICacheServiceAsync
    {
        Task SetAsync<T>(CacheKeySettings keySettings, T value, CancellationToken cancellation);
        Task RemoveAsync(string key, CancellationToken cancellation);
        Task<T> GetAsync<T>(CacheKeySettings keySettings, Func<Task<T>> GetValueFuncAsync, CancellationToken cancellation);
    }
}
