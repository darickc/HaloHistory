using System;
using System.Linq;
using System.Threading.Tasks;
using HaloHistory.Business.Entities;
using HaloHistory.Business.Entities.Stats;
using HaloHistory.Business.Repositories.Metadata;
using HaloSharp;
using HaloSharp.Extension;
using HaloSharp.Model;
using HaloSharp.Model.Stats;
using HaloSharp.Model.Stats.CarnageReport;
using HaloSharp.Model.Stats.CarnageReport.Common;
using HaloSharp.Model.Stats.Common;
using HaloSharp.Model.Stats.Lifetime;
using HaloSharp.Query.Stats;
using HaloSharp.Query.Stats.CarnageReport;
using HaloSharp.Query.Stats.Lifetime;

namespace HaloHistory.Business.Repositories.Stats
{
    public class StatsRepository : IStatsRepository
    {
        private readonly ICartographerContext _db;
        private readonly IHaloSession _session;
        private readonly IMetadataRepository _metadataRepository;

        public StatsRepository(ICartographerContext context, IHaloSession session, IMetadataRepository metadataRepository)
        {
            _session = session;
            _db = context;
            _metadataRepository = metadataRepository;
        }

        public async Task Initialize()
        {
            await _metadataRepository.Initialize();
        }

        public async Task<MatchSet> GetMatchesForPlayer(string gamertag, int start = 0, int count = 10, params Enumeration.GameMode[] gameModes)
        {
            var query = new GetMatches().ForPlayer(gamertag).Skip(start).Take(count);
            if (gameModes != null && gameModes.Length > 0)
            {
                if (gameModes.Length == 1)
                {
                    query.InGameMode(gameModes[0]);
                }
                else
                {
                    query.InGameModes(gameModes.ToList());
                }
            }

            var matches = await _session.Query(query);

            foreach (var match in matches.Results)
            {
                match.Map = await _metadataRepository.GetMap(match.MapId);
                if (match.MapVariant != null && match.MapVariant.ResourceType == Enumeration.ResourceType.MapVariant &&
                    match.MapVariant.OwnerType == Enumeration.OwnerType.Official)
                    match.MapVariantInfo = await _metadataRepository.GetMapVariant(match.MapVariant.ResourceId);

                match.GameBaseVariant = await _metadataRepository.GetGameBaseVariant(match.GameBaseVariantId);
                if (match.GameVariant != null && match.GameVariant.ResourceType == Enumeration.ResourceType.GameVariant &&
                    match.GameVariant.OwnerType == Enumeration.OwnerType.Official)
                    match.GameVariantInfo = await _metadataRepository.GetGameVariant(match.GameVariant.ResourceId);

                if (match.HopperId.HasValue)
                    match.Playlist = await _metadataRepository.GetPlaylist(match.HopperId.Value);

                if (match.SeasonId.HasValue)
                    match.Season = await _metadataRepository.GetSeason(match.SeasonId);

                if (match.Teams != null)
                {
                    foreach (var team in match.Teams)
                    {
                        team.TeamColor = await _metadataRepository.GetTeamColor(team.Id);
                    }
                }
            }

            return matches;
        }

        public async Task<MatchEvents> GetEventsForMatch(Guid id)
        {
            var data = await _db.FindAsync<MatchEventsData>(id.ToString());
            if (data == null)
            {
                var query = new GetMatchEvents().ForMatchId(id);
                var match = await _session.Query(query);
                if (match == null)
                    return null;

                data = new MatchEventsData(id, match);
                _db.InsertAsync(data);
                return match;
            }
            return data.Deserialize();
        }

        public async Task<ArenaMatch> GetArenaMatch(Guid id)
        {
            ArenaMatchData data = await _db.FindAsync<ArenaMatchData>(id.ToString()); 
            
            if (data == null)
            {
                var query = new GetArenaMatchDetails().ForMatchId(id);
                var match = await _session.Query(query);
                if (match == null)
                    return null;

                await PopulateBaseMatch(match);

                foreach (var teamStat in match.TeamStats)
                {
                    teamStat.TeamColor = await _metadataRepository.GetTeamColor(teamStat.TeamId);
                }

                foreach (var playerStat in match.PlayerStats)
                {
                    if (playerStat.CurrentCsr == null)
                    {
                        playerStat.CurrentCsr = new CompetitiveSkillRanking
                        {
                            DesignationId = Enumeration.CompetitiveSkillRankingDesignation.Unranked,
                            Tier = 10 - playerStat.MeasurementMatchesLeft
                        };
                    }

                    var csr = await _metadataRepository.GetCompetitiveSkillRankDesignation((int)playerStat.CurrentCsr.DesignationId);
                    var tier = csr.Tiers.SingleOrDefault(t => t.Id == (playerStat.CurrentCsr.Tier));
                    csr.Tiers = null;
                    playerStat.CurrentCsr.Designation = csr;
                    playerStat.CurrentCsr.CurrentTier = tier;
                    playerStat.CurrentSpartanRank = await _metadataRepository.GetSpartanRank(playerStat.XpInfo.SpartanRank);
                    playerStat.NextSpartanRank = await _metadataRepository.GetSpartanRank(playerStat.XpInfo.SpartanRank+1);

                    if (playerStat.PreviousCsr == null)
                    {
                        playerStat.PreviousCsr = new CompetitiveSkillRanking
                        {
                            DesignationId = Enumeration.CompetitiveSkillRankingDesignation.Unranked,
                            Tier = playerStat.MeasurementMatchesLeft != 10 ? 10 - playerStat.MeasurementMatchesLeft - 1 : -1
                        };
                    }

                    csr = await _metadataRepository.GetCompetitiveSkillRankDesignation((int)playerStat.PreviousCsr.DesignationId);
                    tier = csr.Tiers.SingleOrDefault(t => t.Id == (playerStat.PreviousCsr.Tier));
                    csr.Tiers = null;
                    playerStat.PreviousCsr.Designation = csr;
                    playerStat.PreviousCsr.CurrentTier = tier;

                    await PopulatePlayer(playerStat);
                }

                data = new ArenaMatchData(id, match);
                _db.InsertAsync(data);
                return match; 
            }
            return data.Deserialize();
        }

        public async Task<CampaignMatch> GetCampaignMatch(Guid id)
        {
            var data = await _db.FindAsync<CampaignMatchData>(id.ToString());

            if (data == null)
            {
                var query = new GetCampaignMatchDetails().ForMatchId(id);
                var match = await _session.Query(query);
                if (match == null)
                    return null;

                await PopulateBaseMatch(match);
                
                foreach (var playerStat in match.PlayerStats)
                {
                    await PopulatePlayer(playerStat);
                }

                data = new CampaignMatchData(id, match);
                _db.InsertAsync(data);
                return match;
            }
            return data.Deserialize();
        }

        public async Task<CustomMatch> GetCustomMatch(Guid id)
        {
            CustomMatchData data = await _db.FindAsync<CustomMatchData>(id.ToString());

            if (data == null)
            {
                var query = new GetCustomMatchDetails().ForMatchId(id);
                var match = await _session.Query(query);
                if (match == null)
                    return null;

                await PopulateBaseMatch(match);

                foreach (var teamStat in match.TeamStats)
                {
                    teamStat.TeamColor = await _metadataRepository.GetTeamColor(teamStat.TeamId);
                }

                foreach (var playerStat in match.PlayerStats)
                {
                    await PopulatePlayer(playerStat);
                }

                data = new CustomMatchData(id, match);
                _db.InsertAsync(data);
                return match;
            }
            return data.Deserialize();
        }

        public async Task<WarzoneMatch> GetWarzoneMatch(Guid id)
        {
            WarzoneMatchData data = await _db.FindAsync<WarzoneMatchData>(id.ToString());

            if (data == null)
            {
                var query = new GetWarzoneMatchDetails().ForMatchId(id);
                var match = await _session.Query(query);
                if (match == null)
                    return null;

                await PopulateBaseMatch(match);

                foreach (var teamStat in match.TeamStats)
                {
                    teamStat.TeamColor = await _metadataRepository.GetTeamColor(teamStat.TeamId);
                }

                foreach (var playerStat in match.PlayerStats)
                {
                    playerStat.CurrentSpartanRank = await _metadataRepository.GetSpartanRank(playerStat.XpInfo.SpartanRank);
                    playerStat.NextSpartanRank = await _metadataRepository.GetSpartanRank(playerStat.XpInfo.SpartanRank + 1);
                    await PopulatePlayer(playerStat);
                }

                data = new WarzoneMatchData(id, match);
                _db.InsertAsync(data);
                return match;
            }
            return data.Deserialize();
        }

        public async Task<ArenaServiceRecord> GetArenaServiceRecord(string gamertag, Guid? seasonId)
        {
            var query = new GetArenaServiceRecord().ForPlayer(gamertag);
            if (seasonId.HasValue)
                query = query.ForSeasonId(seasonId.Value);
            var record = await _session.Query(query);

            return record;
        }

        public async Task<CampaignServiceRecord> GetCampaignServiceRecord(string gamertag)
        {
            var query = new GetCampaignServiceRecord().ForPlayer(gamertag);
            var record = await _session.Query(query);
            return record;
        }

        public async Task<CustomServiceRecord> GetCustomServiceRecord(string gamertag)
        {
            var query = new GetCustomServiceRecord().ForPlayer(gamertag);
            var record = await _session.Query(query);

            return record;
        }

        public async Task<WarzoneServiceRecord> GetWarzoneServiceRecord(string gamertag)
        {
            var query = new GetWarzoneServiceRecord().ForPlayer(gamertag);
            var record = await _session.Query(query);

            return record;
        }

        private async Task PopulateBaseMatch(BaseMatch match)
        {
            match.Map = await _metadataRepository.GetMap(match.MapId);
            match.Playlist = await _metadataRepository.GetPlaylist(match.PlaylistId);
            match.MapVariantInfo = await _metadataRepository.GetMapVariant(match.MapVariantId);
            match.Season = await _metadataRepository.GetSeason(match.SeasonId);
            match.GameBaseVariant = await _metadataRepository.GetGameBaseVariant(match.GameBaseVariantId);
            match.GameVariantInfo = await _metadataRepository.GetGameVariant(match.GameVariantId);
        }

        private async Task PopulatePlayer(BasePlayerStat playerStat)
        {
            if (playerStat.WeaponWithMostKills != null)
                playerStat.WeaponWithMostKills.Weapon = await _metadataRepository.GetWeapon(playerStat.WeaponWithMostKills.WeaponId.StockId);

            foreach (var weaponStat in playerStat.WeaponStats)
            {
                weaponStat.Weapon = await _metadataRepository.GetWeapon(weaponStat.WeaponId.StockId);
            }

            foreach (var medalAward in playerStat.MedalAwards)
            {
                medalAward.Medal = await _metadataRepository.GetMedal(medalAward.MedalId);
            }

            foreach (var enemyKill in playerStat.EnemyKills)
            {
                enemyKill.Enemy.EnemyObject = await _metadataRepository.GetEnemy(enemyKill.Enemy.BaseId);
            }

            foreach (var vehicle in playerStat.DestroyedEnemyVehicles)
            {
                vehicle.Enemy.Vehicle = await _metadataRepository.GetVehicle(vehicle.Enemy.BaseId);
            }
        }
    }
}
