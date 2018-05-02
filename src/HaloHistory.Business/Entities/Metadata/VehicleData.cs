using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class VehicleData : BaseDataEntity<long, Vehicle>
    {
        public VehicleData()
        {
        }

        public VehicleData(long id, Vehicle data) : base(id, data)
        {
        }
    }
}
