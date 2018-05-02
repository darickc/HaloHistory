using System;
using HaloSharp.Model.Stats.CarnageReport;

namespace HaloHistory.Business.Entities.Stats
{
    public class WarzoneMatchData : BaseDataEntity<string, WarzoneMatch>
    {
        public WarzoneMatchData()
        {
        }

        public WarzoneMatchData(Guid id, WarzoneMatch data) : base(id.ToString(), data)
        {
        }
    }
}
