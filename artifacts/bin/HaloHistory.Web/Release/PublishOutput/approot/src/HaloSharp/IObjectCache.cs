using System;

namespace HaloSharp
{
    public interface IObjectCache
    {
        void Add<T>(string key, T toAdd, DateTime expiration) where T : class;

        bool Contains(string key);
        
        T Get<T>(string key) where T : class;
    }
}
