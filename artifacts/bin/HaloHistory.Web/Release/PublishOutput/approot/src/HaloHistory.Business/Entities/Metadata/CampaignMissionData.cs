using System;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class CampaignMissionData : BaseDataEntity<string, CampaignMission>
    {
        public CampaignMissionData()
        {
        }

        public CampaignMissionData(Guid id, CampaignMission data) : base(id.ToString(), data)
        {
        }
    }
}
