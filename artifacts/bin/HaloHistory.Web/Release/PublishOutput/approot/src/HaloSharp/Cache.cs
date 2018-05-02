using System;
//using System.Runtime.Caching;

namespace HaloSharp
{
    public static class Cache
    {
        //private static readonly ObjectCache ObjectCache = MemoryCache.Default;
        private static readonly object LockObject = new object();

        public static TimeSpan? MetadataCacheDuration { get; set; }
        public static TimeSpan? ProfileCacheDuration { get; set; }
        public static TimeSpan? StatsCacheDuration { get; set; }

        public static IObjectCache ObjectCache { get; set; }

        public static void AddMetadata<T>(string key, T toAdd) where T : class
        {
            if (ObjectCache == null)
                return;
            lock (LockObject)
            {
                if (toAdd != null && MetadataCacheDuration.HasValue)
                {
                    var absoluteExpiration = DateTime.UtcNow.Add(MetadataCacheDuration.Value);
                    ObjectCache.Add(key, toAdd, absoluteExpiration);
                }
            }
        }

        public static void AddProfile<T>(string key, T toAdd) where T : class
        {
            if (ObjectCache == null)
                return;
            lock (LockObject)
            {
                if (toAdd != null && ProfileCacheDuration.HasValue)
                {
                    var absoluteExpiration = DateTime.UtcNow.Add(ProfileCacheDuration.Value);
                    ObjectCache.Add(key, toAdd, absoluteExpiration);
                }
            }
        }

        public static void AddStats<T>(string key, T toAdd) where T : class
        {
            if (ObjectCache == null)
                return;
            lock (LockObject)
            {
                if (toAdd != null && StatsCacheDuration.HasValue)
                {
                    var absoluteExpiration = DateTime.UtcNow.Add(StatsCacheDuration.Value);
                    ObjectCache.Add(key, toAdd, absoluteExpiration);
                }
            }
        }

        public static T Get<T>(string key) where T : class
        {
            if (ObjectCache == null)
                return null;
            lock (LockObject)
            {
                if (ObjectCache.Contains(key))
                {
                    //return (T) ObjectCache[key];
                    return ObjectCache.Get<T>(key);
                }
                return null;
            }
        }
    }
}