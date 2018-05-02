using System;
using System.Threading.Tasks;
using HaloSharp.Model;
using HaloSharp.Model.Stats;
using HaloSharp.Model.Stats.CarnageReport;
using HaloSharp.Model.Stats.Lifetime;

namespace HaloHistory.Business.Repositories.Stats
{
    public interface IStatsRepository
    {
        Task<ArenaMatch> GetArenaMatch(Guid id);
        Task<ArenaServiceRecord> GetArenaServiceRecord(string gamertag, Guid? seasonId);
        Task<CampaignMatch> GetCampaignMatch(Guid id);
        Task<CampaignServiceRecord> GetCampaignServiceRecord(string gamertag);
        Task<CustomMatch> GetCustomMatch(Guid id);
        Task<CustomServiceRecord> GetCustomServiceRecord(string gamertag);
        Task<MatchEvents> GetEventsForMatch(Guid id);
        Task<MatchSet> GetMatchesForPlayer(string gamertag, int start = 0, int count = 10, params Enumeration.GameMode[] gameModes);
        Task<WarzoneMatch> GetWarzoneMatch(Guid id);
        Task<WarzoneServiceRecord> GetWarzoneServiceRecord(string gamertag);
        Task Initialize();
    }
}