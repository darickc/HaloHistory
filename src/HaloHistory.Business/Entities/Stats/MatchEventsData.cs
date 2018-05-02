using System;
using HaloSharp.Model.Stats.CarnageReport;

namespace HaloHistory.Business.Entities.Stats
{
    public class MatchEventsData : BaseDataEntity<string, MatchEvents>
    {
        public MatchEventsData()
        {
        }

        public MatchEventsData(Guid id, MatchEvents data) : base(id.ToString(), data)
        {
        }
    }
}
