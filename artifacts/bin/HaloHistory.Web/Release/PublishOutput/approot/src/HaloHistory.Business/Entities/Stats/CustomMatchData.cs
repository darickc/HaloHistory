using System;
using HaloSharp.Model.Stats.CarnageReport;

namespace HaloHistory.Business.Entities.Stats
{
    public class CustomMatchData : BaseDataEntity<string, CustomMatch>
    {
        public CustomMatchData()
        {
        }

        public CustomMatchData(Guid id, CustomMatch data) : base(id.ToString(), data)
        {
        }
    }
}
