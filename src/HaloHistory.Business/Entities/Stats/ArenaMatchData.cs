using System;
using HaloSharp.Model.Stats.CarnageReport;

namespace HaloHistory.Business.Entities.Stats
{
    public class ArenaMatchData : BaseDataEntity<string, ArenaMatch>
    {
        public ArenaMatchData()
        {
        }

        public ArenaMatchData(Guid id, ArenaMatch data) : base(id.ToString(), data)
        {
        }
    }
}
