using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class WeaponData : BaseDataEntity<long, Weapon>
    {
        public WeaponData()
        {
        }

        public WeaponData(long id, Weapon data) : base(id, data)
        {
        }
    }
}
