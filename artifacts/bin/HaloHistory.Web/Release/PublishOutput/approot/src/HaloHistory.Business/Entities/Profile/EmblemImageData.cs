using System;

namespace HaloHistory.Business.Entities.Profile
{
    public class EmblemImageData : BaseDataEntity<string, string>
    {
        public DateTime Timestamp { get; set; }

        public EmblemImageData()
        {
        }

        public EmblemImageData(string id, string data, DateTime timestamp)
        {
            ItemId = id;
            Data = data;
            Timestamp = timestamp;
        }
    }
}
