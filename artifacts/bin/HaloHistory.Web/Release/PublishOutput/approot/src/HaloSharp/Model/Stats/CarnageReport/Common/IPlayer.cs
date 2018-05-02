using System.Collections.Generic;

namespace HaloSharp.Model.Stats.CarnageReport.Common
{
    public interface IPlayer : IBasePlayerStat
    {
        List<OpponentDetails> KilledByOpponentDetails { get; set; }
        List<OpponentDetails> KilledOpponentDetails { get; set; }
    }
}
