using System;
using System.Collections.Generic;
using System.Linq;
using HaloHistory.Business.Entities;
using HaloSharp;

namespace HaloHistory.Web.Services
{
    public class DataCache : IDataCache
    {
        private readonly IObjectCache _objectCache;
        public DataCache(IObjectCache objectCache)
        {
            _objectCache = objectCache;
        }

        public bool Contains<T,T2,T3>() where T : BaseDataEntity<T2, T3>
        {
            return _objectCache.Contains(typeof (T).Name);
        }

        public bool Contains<T, T2>(T2 id) where T : class
        {
            if (_objectCache.Contains(typeof (T).Name))
            {
                var dictionary = _objectCache.Get<Dictionary<T2, T>>(typeof (T).Name);
                return dictionary.ContainsKey(id);
            }
            return false;
        }

        public void Add<T, T2, T3>(List<T> items) where T : BaseDataEntity<T2, T3>
        {
            Dictionary<T2,T> dictionary = items.ToDictionary(item=>item.ItemId);
            _objectCache.Add(typeof(T).Name, dictionary,DateTime.Now.AddDays(2));
        }

        public void Add<T, T2, T3>(T2 id, T item) where T : BaseDataEntity<T2, T3>
        {
            var dictionary = _objectCache.Get<Dictionary<T2, T>>(typeof(T).Name);
            dictionary.Add(id,item);
            //Dictionary<T2, T> dictionary = items.ToDictionary(item => item.ItemId);
            _objectCache.Add(typeof(T).Name, dictionary, DateTime.Now.AddDays(2));
        }

        public void Add<T, T2>(T2 key, T item) where T : class
        {
            if (_objectCache.Contains(typeof (T).Name))
            {
                var dictionary = _objectCache.Get<Dictionary<T2, T>>(typeof (T).Name);
                dictionary[key] = item;
            }
            else
            {
                var dictionary = new Dictionary<T2, T> {[key] = item};
                _objectCache.Add(typeof(T).Name,dictionary,DateTime.Now.AddDays(2));
            }
        }

        public T Get<T, T2, T3>(T2 id) where T : BaseDataEntity<T2, T3>
        {
            var dictionary = _objectCache.Get<Dictionary<T2, T>>(typeof (T).Name);
            if (dictionary.ContainsKey(id))
            {
                return dictionary[id];
            }
            return null;
        }

        public T Get<T, T2>(T2 id) where T : class
        {
            var dictionary = _objectCache.Get<Dictionary<T2, T>>(typeof(T).Name);
            return dictionary[id];
        }
    }
}
