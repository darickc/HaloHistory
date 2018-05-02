using HaloHistory.Business.Models.Stats;

namespace HaloHistory.Business.Entities.Stats
{
    public class MatchResultData : BaseDataEntity<string, MatchResult>
    {
        public MatchResultData()
        {
        }

        public MatchResultData(string id, MatchResult data) : base(id, data)
        {
        }
    }
}
