using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class CompetitiveSkillRankDesignationData : BaseDataEntity<int, CompetitiveSkillRankDesignation>
    {
        public CompetitiveSkillRankDesignationData()
        {
        }

        public CompetitiveSkillRankDesignationData(int id, CompetitiveSkillRankDesignation data) : base(id, data)
        {
        }
    }
}
