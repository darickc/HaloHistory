using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class TeamColorData : BaseDataEntity<int, TeamColor>
    {
        public TeamColorData()
        {
        }

        public TeamColorData(int id, TeamColor data) : base(id, data)
        {
        }
    }
}
