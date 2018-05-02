using System;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class MapData : BaseDataEntity<string, Map>
    {
        public MapData()
        {
        }

        public MapData(Guid id, Map data) : base(id.ToString(), data)
        {
        }
    }
}
