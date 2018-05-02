using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class ImpulseData : BaseDataEntity<long, Impulse>
    {
        public ImpulseData()
        {
        }

        public ImpulseData(long id, Impulse data) : base(id, data)
        {
        }
    }
}
