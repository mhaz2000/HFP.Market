using Microsoft.Extensions.Caching.Memory;

namespace HFP.Infrastructure.Caching
{
    internal sealed class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
            => _cache = cache;
        public T? Get<T>(string key)
            => _cache.TryGetValue(key, out T? value) ? value : default;

        public void Remove(string key)
            => _cache.Remove(key);

        public void Set(string key, object value, TimeSpan expiration)
            => _cache.Set(key, value, expiration);

    }
}
