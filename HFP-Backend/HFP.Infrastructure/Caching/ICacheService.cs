﻿namespace HFP.Infrastructure.Caching
{
    public interface ICacheService
    {
        void Set(string key, object value, TimeSpan expiration);
        T? Get<T>(string key);
        void Remove(string key);
    }
}
