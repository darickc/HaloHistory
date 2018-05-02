using System;

namespace HaloSharp.Model
{
    public interface ICacheSettings
    {
        TimeSpan? MetadataCacheDuration { get; set; }
        TimeSpan? ProfileCacheDuration { get; set; }
        TimeSpan? StatsCacheDuration { get; set; }
    }
}