using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class SkullData : BaseDataEntity<int, Skull>
    {
        public SkullData()
        {
        }

        public SkullData(int id, Skull data) : base(id, data)
        {
        }
    }
}
