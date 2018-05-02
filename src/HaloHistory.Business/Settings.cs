namespace HaloHistory.Business
{
    public class Settings : ISettings
    {
        public bool CacheResults { get; set; }
        public string ApiKey { get; set; }
        public int RequestCount { get; set; }
        public int RequestTime { get; set; }
    }
}
