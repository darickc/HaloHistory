using System;
using System.Threading.Tasks;
using HaloHistory.Business.Models.Stats;
using HaloSharp.Model;

namespace HaloHistory.Business
{
    public interface IStatsBusiness
    {
        Task<MatchSet> GetMatchesForPlayer(string gamertag, int start = 0, params Enumeration.GameMode[] gameModes);

        Task<string> GetMatch(Enumeration.GameMode gameMode, Guid id, string gamerTag);
    }
}