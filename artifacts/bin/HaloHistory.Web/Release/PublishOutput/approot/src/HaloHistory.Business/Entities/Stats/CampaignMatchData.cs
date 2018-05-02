using System;
using HaloSharp.Model.Stats.CarnageReport;

namespace HaloHistory.Business.Entities.Stats
{
    public class CampaignMatchData : BaseDataEntity<string, CampaignMatch>
    {
        public CampaignMatchData()
        {
        }

        public CampaignMatchData(Guid id, CampaignMatch data) : base(id.ToString(), data)
        {
        }
    }
}
