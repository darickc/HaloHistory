using System;
using HaloSharp;
using Microsoft.Extensions.Caching.Memory;

namespace HaloHistory.Web.Services
{
    public class HaloObjectCache : IObjectCache
    {
        private readonly IMemoryCache  _objectCache;

        public HaloObjectCache()
        {
            _objectCache = new MemoryCache(new MemoryCacheOptions());
        }

        public void Add<T>(string key, T toAdd, DateTime expiration) where T : class
        {
            _objectCache.Set(key, toAdd,new MemoryCacheEntryOptions{AbsoluteExpiration = expiration});
        }

        public bool Contains(string key)
        {
            object temp;
            return _objectCache.TryGetValue(key,out temp);
        }

        public T Get<T>(string key) where T : class
        {
            T temp;
            _objectCache.TryGetValue(key,out temp);
            return temp;
        }
    }
}
