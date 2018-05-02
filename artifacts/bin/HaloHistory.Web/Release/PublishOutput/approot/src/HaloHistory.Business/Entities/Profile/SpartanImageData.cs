using System;

namespace HaloHistory.Business.Entities.Profile
{
    public class SpartanImageData : BaseDataEntity<string, string>
    {
        public DateTime Timestamp { get; set; }

        public SpartanImageData()
        {
        }

        public SpartanImageData(string id, string data, DateTime timestamp)
        {
            ItemId = id;
            Data = data;
            Timestamp = timestamp;
        }
    }
}
