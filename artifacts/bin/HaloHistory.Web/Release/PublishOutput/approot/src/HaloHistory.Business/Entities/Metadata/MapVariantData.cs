using System;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class MapVariantData : BaseDataEntity<string, MapVariant>
    {
        public MapVariantData()
        {
        }

        public MapVariantData(Guid id, MapVariant data) : base(id.ToString(), data)
        {
        }
    }
}
