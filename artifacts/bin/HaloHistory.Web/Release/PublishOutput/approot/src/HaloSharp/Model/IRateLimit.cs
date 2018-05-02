using System;

namespace HaloSharp.Model
{
    public interface IRateLimit
    {
        int RequestCount { get; set; }
        TimeSpan Timeout { get; set; }
        TimeSpan TimeSpan { get; set; }
    }
}