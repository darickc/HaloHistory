using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class MedalData : BaseDataEntity<long, Medal>
    {
        public MedalData()
        {
        }

        public MedalData(long id, Medal data) : base(id, data)
        {
        }
    }
}
