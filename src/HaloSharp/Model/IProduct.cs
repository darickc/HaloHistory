namespace HaloSharp.Model
{
    public interface IProduct
    {
        RateLimit RateLimit { get; set; }
        string SubscriptionKey { get; set; }
    }
}