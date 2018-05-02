using System;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class FlexibleStatData : BaseDataEntity<string, FlexibleStat>
    {
        public FlexibleStatData()
        {
        }

        public FlexibleStatData(Guid id, FlexibleStat data) : base(id.ToString(), data)
        {
        }
    }
}
