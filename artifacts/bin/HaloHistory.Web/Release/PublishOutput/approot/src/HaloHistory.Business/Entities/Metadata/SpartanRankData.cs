using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class SpartanRankData : BaseDataEntity<int, SpartanRank>
    {
        public SpartanRankData()
        {
        }

        public SpartanRankData(int id, SpartanRank data) : base(id, data)
        {
        }
    }
}
