using System;

namespace HaloSharp.Model
{
    public class Product : IProduct
    {
        public string SubscriptionKey { get; set; }
        public RateLimit RateLimit { get; set; }
    }

    public class RateLimit : IRateLimit
    {
        public int RequestCount { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}
