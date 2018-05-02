using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaloHistory.Business.Entities;
using HaloHistory.Business.Entities.Stats;
using HaloHistory.Business.Models.Stats;
using HaloHistory.Business.Models.Stats.Common;
using HaloHistory.Business.Repositories.Metadata;
using HaloHistory.Business.Repositories.Stats;
using HaloHistory.Business.Utilities;
using HaloSharp.Model;
using HaloSharp.Model.Stats.CarnageReport;
using HaloSharp.Model.Stats.CarnageReport.Common;

namespace HaloHistory.Business
{
    public class StatsBusiness : IStatsBusiness
    {
        private readonly IStatsRepository _statsRepository;
        private readonly IMetadataRepository _metadataRepository;
        private readonly ICartographerContext _db;
        private readonly ISettings _settings;

        public StatsBusiness(IStatsRepository statsRepository, IMetadataRepository metadataRepository, ICartographerContext db, ISettings settings)
        {
            _statsRepository = statsRepository;
            _metadataRepository = metadataRepository;
            _db = db;
            _settings = settings;
        }

        public async Task<MatchSet> GetMatchesForPlayer(string gamertag, int start = 0, params Enumeration.GameMode[] gameModes)
        {
            var playerMatches = await _statsRepository.GetMatchesForPlayer(gamertag, start, 10, gameModes);
            var matches = Mapper.Map<MatchSet>(playerMatches);

            foreach (var result in matches.Results)
            {
                result.Teams = result.Teams.OrderBy(t => t.Rank).ToList();
                result.PlayerTeamColor = result.Teams.Where(t => t.Id == result.Player.TeamId).Select(t=>t.TeamColor).FirstOrDefault();
            }
            await _db.CommitChanges();
            return matches;
        }

        public async Task<string> GetMatch(Enumeration.GameMode gameMode, Guid id, string gamerTag)
        {
            if (_settings.CacheResults)
            {
                var result = await _db.FindAsync<MatchResultData>(id.ToString());
                if (result != null)
                {
                    return result.Data;
                }
            }
            MatchEvents matchEvents = await _statsRepository.GetEventsForMatch(id);
            matchEvents.GameEvents = matchEvents.GameEvents.Where(e => e.EventName == Enumeration.EventType.Death).ToList();
            MatchResult match = null;
            switch (gameMode)
            {
                case Enumeration.GameMode.Arena:
                    var arenaMatch = await _statsRepository.GetArenaMatch(id);
                    match = await GetMatchResult(arenaMatch, gamerTag, matchEvents);
                    break;
                case Enumeration.GameMode.Campaign:
                    var campaignMatch = await _statsRepository.GetCampaignMatch(id);
                    match = GetMatchResult(campaignMatch, gamerTag, matchEvents);
                    break;
                case Enumeration.GameMode.Custom:
                    var customMatch = await _statsRepository.GetCustomMatch(id);
                    match = await GetMatchResult(customMatch, gamerTag, matchEvents);
                    break;
                case Enumeration.GameMode.Warzone:
                    var warzoneMatch = await _statsRepository.GetWarzoneMatch(id);
                    match = await GetMatchResult(warzoneMatch, gamerTag, matchEvents);
                    break;
                default:
                    return null;
            }

            if (match != null)
            {
                match.GameMode = gameMode;
                match.Id = id;
            }

            var matchResult = new MatchResultData(id.ToString(),match);
            if (_settings.CacheResults)
            {
                _db.InsertAsync(matchResult);
                await _db.CommitChanges();
            }
            return matchResult.Data;
        }


        private async Task<MatchResult> GetMatchResult(ArenaMatch match, string gamerTag, MatchEvents matchEvents)
        {
            MatchResult result = Mapper.Map<MatchResult>(match);
            var players = new List<ArenaPlayer>();
            foreach (var playerStat in match.PlayerStats)
            {
                var p = await GetArenaPlayer(playerStat, matchEvents);
                players.Add(p);
            }
            SetTeams(match, players, result,gamerTag);
            return result;
        }

        private async Task<MatchResult> GetMatchResult(CustomMatch match, string gamerTag, MatchEvents matchEvents)
        {
            MatchResult result = Mapper.Map<MatchResult>(match);
            var players = new List<ArenaPlayer>();
            foreach (var playerStat in match.PlayerStats)
            {
                var p = await GetCustomPlayer(playerStat, matchEvents);
                players.Add(p);
            }
            SetTeams(match, players, result, gamerTag);
            return result;
        }

        private async Task<MatchResult> GetMatchResult(WarzoneMatch match, string gamerTag, MatchEvents matchEvents)
        {
            MatchResult result = Mapper.Map<MatchResult>(match);
            var players = new List<ArenaPlayer>();
            foreach (var playerStat in match.PlayerStats)
            {
                var p = await GetWarzonePlayer(playerStat, matchEvents);
                players.Add(p);
            }

            SetTeams(match, players, result, gamerTag);



            return result;
        }

        private MatchResult GetMatchResult(CampaignMatch match, string gamerTag, MatchEvents matchEvents)
        {
            MatchResult result = Mapper.Map<MatchResult>(match);
            //var players = match.PlayerStats.Select(GetCampaignPlayer).ToList();
            return result;
        }

        private async Task<ArenaPlayer> GetArenaPlayer(ArenaMatchPlayerStat playerStat, MatchEvents matchEvents)
        {
            ArenaPlayer player = Mapper.Map<ArenaPlayer>(playerStat);
            await SetCompetetivePlayer(playerStat, player,matchEvents);

            if (playerStat.CurrentCsr != null)
            {
                player.CurrentCsr = new CompetitiveSkillRanking
                {
                    Name = playerStat.CurrentCsr?.Designation?.Name,
                    BannerImageUrl = playerStat.CurrentCsr?.Designation?.BannerImageUrl,
                    Tier = playerStat.CurrentCsr?.Tier,
                    IconImageUrl = playerStat.CurrentCsr?.CurrentTier?.IconImageUrl,
                    PercentToNextTier = playerStat.CurrentCsr?.PercentToNextTier,
                    Rank = playerStat.CurrentCsr?.Rank,
                    DesignationId = (int?)playerStat.CurrentCsr?.DesignationId ?? 0,
                    Increased = true,
                    Csr = player.CurrentCsr?.Csr
                };

                if (player.PreviousCsr != null)
                {
                    player.PreviousCsr = new CompetitiveSkillRanking
                    {
                        Name = playerStat.PreviousCsr?.Designation?.Name,
                        BannerImageUrl = playerStat.PreviousCsr?.Designation?.BannerImageUrl,
                        Tier = playerStat.PreviousCsr?.Tier,
                        IconImageUrl = playerStat.PreviousCsr?.CurrentTier?.IconImageUrl,
                        PercentToNextTier = playerStat.PreviousCsr?.PercentToNextTier,
                        Rank = playerStat.PreviousCsr?.Rank,
                        DesignationId = (int?)playerStat.PreviousCsr?.DesignationId ?? 0,
                        Csr = player.PreviousCsr?.Csr
                    };

                    player.CurrentCsr.Increased = (player.CurrentCsr.DesignationId > player.PreviousCsr.DesignationId) ||
                        (player.CurrentCsr.DesignationId == player.PreviousCsr.DesignationId && player.CurrentCsr.Tier > player.PreviousCsr.Tier) ||
                        (player.CurrentCsr.DesignationId == player.PreviousCsr.DesignationId && player.CurrentCsr.Tier == player.PreviousCsr.Tier && player.CurrentCsr.PercentToNextTier > player.PreviousCsr.PercentToNextTier);

                    if (player.CurrentCsr.Increased &&
                        (player.CurrentCsr.DesignationId > player.PreviousCsr.DesignationId ||
                         player.CurrentCsr.Tier > player.PreviousCsr.Tier))
                        player.PreviousCsr.PercentToNextTier = 0;

                    if (!player.CurrentCsr.Increased &&
                       (player.CurrentCsr.DesignationId < player.PreviousCsr.DesignationId ||
                        player.CurrentCsr.Tier < player.PreviousCsr.Tier))
                        player.PreviousCsr.PercentToNextTier = 100;
                }
            }
            

            return player;

            //player.XpInfo.XpEarned = player.XpInfo.TotalXp - player.XpInfo.PrevTotalXp;
            //player.XpInfo.CurrentRankXp = player.XpInfo.TotalXp - playerStat.CurrentSpartanRank.StartXp;
            //var previousRankXp = player.XpInfo.PrevTotalXp - playerStat.CurrentSpartanRank.StartXp;
            //player.XpInfo.XpToNextRank = playerStat.NextSpartanRank.StartXp - playerStat.CurrentSpartanRank.StartXp;
            //player.XpInfo.PreviousPercent = previousRankXp.Percent(player.XpInfo.XpToNextRank);
            //player.XpInfo.CurrentPercent = player.XpInfo.CurrentRankXp.Percent(player.XpInfo.XpToNextRank);


            //var killed = playerStat.KilledOpponentDetails.Select(p => p.GamerTag);
            //var killedBy = playerStat.KilledByOpponentDetails.Select(p => p.GamerTag);
            //var temp = killed.Union(killedBy);

            //foreach (var tag in temp)
            //{
            //    Enemy enemy = new Enemy
            //    {
            //        Name = tag,
            //        Kills = playerStat.KilledOpponentDetails?.FirstOrDefault(p => p.GamerTag == tag)?.TotalKills ?? 0,
            //        Deaths = playerStat.KilledByOpponentDetails?.FirstOrDefault(p => p.GamerTag == tag)?.TotalKills ?? 0,
            //        IsPlayer = true
            //    };
            //    enemy.Spread = enemy.Kills - enemy.Deaths;
            //    player.Enemies.Add(enemy);
            //}

            //var maxSpread = player.Enemies.Max(e => e.Spread);
            //var minSpread = Math.Abs(player.Enemies.Min(e => e.Spread));
            //int minKills = player.Enemies.Min(p => p.Kills);
            //int maxKills = player.Enemies.Max(p => p.Kills) - minKills;
            //int minDeaths = player.Enemies.Min(p => p.Deaths);
            //int maxDeaths = player.Enemies.Max(p => p.Deaths) - minDeaths;
            //foreach (var enemy in player.Enemies)
            //{
            //    if (enemy.Spread > 0)
            //    {
            //        enemy.SpreadSign = "+";
            //        enemy.SpreadPercent = enemy.Spread.Percent(maxSpread);
            //    }
            //    else if (enemy.Spread < 0)
            //    {
            //        enemy.SpreadSign = "-";
            //        enemy.Spread = Math.Abs(enemy.Spread);
            //        enemy.SpreadPercent = enemy.Spread.Percent(minSpread);
            //    }
            //    enemy.KillPercent = enemy.Kills.Percent(minKills, maxKills);
            //    enemy.DeathPercent = enemy.Deaths.Percent(minDeaths, maxDeaths);
            //}

            //player.Enemies = player.Enemies.OrderByDescending(e => e.Kills).ThenBy(e => e.Deaths).ToList();


            //player.Weapons = playerStat.WeaponStats.Where(w => w.Weapon != null).OrderByDescending(w => w.TotalKills).Select(Mapper.Map<WeaponStat>).ToList();

            //minKills = player.Weapons.Min(w => w.TotalKills);
            //maxKills = player.Weapons.Max(w => w.TotalKills) - minKills;
            //var minHeadShots = player.Weapons.Min(w => w.TotalHeadshots);
            //var maxHeadShots = player.Weapons.Max(w => w.TotalHeadshots) - minHeadShots;
            //var minTotalDamage = player.Weapons.Min(w => w.TotalDamageDealt);
            //var maxTotalDamage = player.Weapons.Max(w => w.TotalDamageDealt) - minTotalDamage;
            //var minShotsFired = player.Weapons.Min(w => w.TotalShotsFired);
            //var maxShotsFired = player.Weapons.Max(w => w.TotalShotsFired) - minShotsFired;
            //var minShotsLanded = player.Weapons.Min(w => w.TotalShotsLanded);
            //var maxShotsLanded = player.Weapons.Max(w => w.TotalShotsLanded) - minShotsLanded;
            //var minAccuraccy = player.Weapons.Min(w => w.Accuracy);
            //var maxAccuraccy = player.Weapons.Max(w => w.Accuracy) - minAccuraccy;
            //foreach (var weaponStat in player.Weapons)
            //{
            //    weaponStat.KillPercent = weaponStat.TotalKills.Percent(minKills, maxKills);
            //    weaponStat.HeadShotPercent = weaponStat.TotalHeadshots.Percent(minHeadShots, maxHeadShots);
            //    weaponStat.TotalDamagePercent = weaponStat.TotalDamageDealt.Percent(minTotalDamage, maxTotalDamage);
            //    weaponStat.ShotsFiredPercent = weaponStat.TotalShotsFired.Percent(minShotsFired, maxShotsFired);
            //    weaponStat.ShotsLandedPercent = weaponStat.TotalShotsLanded.Percent(minShotsLanded, maxShotsLanded);
            //    weaponStat.AccuracyPercent = weaponStat.Accuracy.Percent(minAccuraccy, maxAccuraccy);
            //}

            //player.Medals = playerStat.MedalAwards.GroupBy(m => m.Medal.Classification).Select(m =>
            //    new MedalClassification
            //    {
            //        Classification = m.Key,
            //        MedalAwards = Mapper.Map<List<MedalAward>>(m)
            //    }).ToList();


            //return player;
        }

        private async Task<ArenaPlayer> GetCustomPlayer(CustomMatchPlayerStat playerStat, MatchEvents matchEvents)
        {
            ArenaPlayer player = Mapper.Map<ArenaPlayer>(playerStat);
            await SetPlayer(playerStat,player,matchEvents);
            return player;
        }

        private async Task<ArenaPlayer> GetWarzonePlayer(WarzonePlayerStat playerStat, MatchEvents matchEvents)
        {
            ArenaPlayer player = Mapper.Map<ArenaPlayer>(playerStat);
            await SetCompetetivePlayer(playerStat,player, matchEvents);
            return player;
        }

        private async Task<ArenaPlayer> GetCampaignPlayer(CampaignMatchPlayerStat playerStat)
        {
            ArenaPlayer player = Mapper.Map<ArenaPlayer>(playerStat);
            await SetBasePlayer(playerStat, player);
            return player;
        }

        private async Task SetCompetetivePlayer(ICompetetivePlayer playerStat, ArenaPlayer player, MatchEvents matchEvents = null)
        {
            await SetPlayer(playerStat, player, matchEvents);

            player.XpInfo.XpEarned = player.XpInfo.TotalXp - player.XpInfo.PrevTotalXp;
            player.XpInfo.CurrentRankXp = player.XpInfo.TotalXp - playerStat.CurrentSpartanRank.StartXp;
            var previousRankXp = player.XpInfo.PrevTotalXp - playerStat.CurrentSpartanRank.StartXp;
            player.XpInfo.XpToNextRank = playerStat.NextSpartanRank.StartXp - playerStat.CurrentSpartanRank.StartXp;
            player.XpInfo.PreviousPercent = previousRankXp.Percent(player.XpInfo.XpToNextRank);
            player.XpInfo.CurrentPercent = player.XpInfo.CurrentRankXp.Percent(player.XpInfo.XpToNextRank);
        }

        private async Task SetPlayer(IPlayer playerStat, ArenaPlayer player, MatchEvents matchEvents = null)
        {
            await SetBasePlayer(playerStat,player, matchEvents);

            var killed = playerStat.KilledOpponentDetails.Select(p => p.GamerTag);
            var killedBy = playerStat.KilledByOpponentDetails.Select(p => p.GamerTag);
            var temp = killed.Union(killedBy);

            

            foreach (var tag in temp)
            {
                Enemy enemy = new Enemy
                {
                    Name = tag,
                    Kills = playerStat.KilledOpponentDetails?.FirstOrDefault(p => p.GamerTag == tag)?.TotalKills ?? 0,
                    Deaths = playerStat.KilledByOpponentDetails?.FirstOrDefault(p => p.GamerTag == tag)?.TotalKills ?? 0,
                    IsPlayer = true
                };
                enemy.Spread = enemy.Kills - enemy.Deaths;
                if (matchEvents != null)
                {
                    var weaponKills = matchEvents.GameEvents.Where(e=> e.Killer != null && e.Victim != null && 
                        e.KillerWeaponStockId.HasValue && e.EventName == Enumeration.EventType.Death &&
                        ((e.Killer.Gamertag == tag && e.Victim.Gamertag == player.GamerTag) || 
                        (e.Killer.Gamertag == player.GamerTag && e.Victim.Gamertag == tag))).Select(e=> new
                        {
                            Killer = e.Killer.Gamertag,
                            Victim = e.Victim.Gamertag,
                            WeaponId = e.WeaponKillId(),
                            Type = e.SpartanKillType()
                        }).ToList();

                    var groups = weaponKills.GroupBy(g => new { g.WeaponId, g.Type});
                    foreach (var g in groups)
                    {
                        var weapon = await _metadataRepository.GetWeapon(g.Key.WeaponId);
                        var weaponKill = Mapper.Map<WeaponKill>(weapon);
                        if (weaponKill != null)
                        {
                            if (!string.IsNullOrEmpty(g.Key.Type))
                                weaponKill.Name = g.Key.Type;
                            weaponKill.Kills = g.Count(g2=>g2.Killer == player.GamerTag);
                            weaponKill.Deaths = g.Count(g2 => g2.Killer == tag);
                            enemy.WeaponKills.Add(weaponKill);
                        }
                    }
                    enemy.WeaponKills = enemy.WeaponKills.OrderByDescending(e => e.Kills).ThenBy(e => e.Deaths).ToList();
                }

                player.Enemies.Add(enemy);
            }

            if (player.Enemies.Any())
            {
                var maxSpread = player.Enemies.Max(e => e.Spread);
                var minSpread = Math.Abs(player.Enemies.Min(e => e.Spread));
                int minKills = player.Enemies.Min(p => p.Kills);
                int maxKills = player.Enemies.Max(p => p.Kills) - minKills;
                int minDeaths = player.Enemies.Min(p => p.Deaths);
                int maxDeaths = player.Enemies.Max(p => p.Deaths) - minDeaths;
                foreach (var enemy in player.Enemies)
                {
                    if (enemy.Spread > 0)
                    {
                        enemy.SpreadSign = "+";
                        enemy.SpreadPercent = enemy.Spread.Percent(maxSpread);
                    }
                    else if (enemy.Spread < 0)
                    {
                        enemy.SpreadSign = "-";
                        enemy.Spread = Math.Abs(enemy.Spread);
                        enemy.SpreadPercent = enemy.Spread.Percent(minSpread);
                    }
                    enemy.KillPercent = enemy.Kills.Percent(minKills, maxKills);
                    enemy.DeathPercent = enemy.Deaths.Percent(minDeaths, maxDeaths);
                }
            }
            

            player.Enemies = player.Enemies.OrderByDescending(e => e.Kills).ThenBy(e => e.Deaths).ToList();
        }

        private async Task SetBasePlayer(IBasePlayerStat playerStat, ArenaPlayer player, MatchEvents matchEvents = null)
        {
            player.Weapons = playerStat.WeaponStats.Where(w => w.Weapon != null && (w.TotalDamageDealt > 0 || w.TotalShotsFired > 0)).OrderByDescending(w => w.TotalKills).Select(Mapper.Map<WeaponStat>).ToList();
            if (player.Weapons.Any())
            {
                var minKills = player.Weapons.Min(w => w.TotalKills);
                var maxKills = player.Weapons.Max(w => w.TotalKills) - minKills;
                var minHeadShots = player.Weapons.Min(w => w.TotalHeadshots);
                var maxHeadShots = player.Weapons.Max(w => w.TotalHeadshots) - minHeadShots;
                var minTotalDamage = player.Weapons.Min(w => w.TotalDamageDealt);
                var maxTotalDamage = player.Weapons.Max(w => w.TotalDamageDealt) - minTotalDamage;
                var minShotsFired = player.Weapons.Min(w => w.TotalShotsFired);
                var maxShotsFired = player.Weapons.Max(w => w.TotalShotsFired) - minShotsFired;
                var minShotsLanded = player.Weapons.Min(w => w.TotalShotsLanded);
                var maxShotsLanded = player.Weapons.Max(w => w.TotalShotsLanded) - minShotsLanded;
                var minAccuraccy = player.Weapons.Min(w => w.Accuracy);
                var maxAccuraccy = player.Weapons.Max(w => w.Accuracy) - minAccuraccy;
                foreach (var weaponStat in player.Weapons)
                {
                    weaponStat.KillPercent = weaponStat.TotalKills.Percent(minKills, maxKills);
                    weaponStat.HeadShotPercent = weaponStat.TotalHeadshots.Percent(minHeadShots, maxHeadShots);
                    weaponStat.TotalDamagePercent = weaponStat.TotalDamageDealt.Percent(minTotalDamage, maxTotalDamage);
                    weaponStat.ShotsFiredPercent = weaponStat.TotalShotsFired.Percent(minShotsFired, maxShotsFired);
                    weaponStat.ShotsLandedPercent = weaponStat.TotalShotsLanded.Percent(minShotsLanded, maxShotsLanded);
                    weaponStat.AccuracyPercent = weaponStat.Accuracy.Percent(minAccuraccy, maxAccuraccy);
                }
            }

            player.Medals = playerStat.MedalAwards?.Where(m=>m.Medal != null).GroupBy(m => m.Medal.Classification).Select(m =>
                new MedalClassification
                {
                    Classification = m.Key,
                    MedalAwards = Mapper.Map<List<MedalAward>>(m)
                }).ToList();

            player.TotalPerfectKills = playerStat.FlexibleStats.ImpulseStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.PerfectKill))?.Count ?? 0;
            player.RoundWinningKills = playerStat.FlexibleStats.ImpulseStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.RoundWinningKill))?.Count ?? 0;
            player.FlagCaptures = playerStat.FlexibleStats.ImpulseStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.FlagCapture))?.Count ?? 0;
            player.FlagPulls = playerStat.FlexibleStats.ImpulseStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.FlagGrabs))?.Count ?? 0;
            player.FlagCarrierKills = playerStat.FlexibleStats.ImpulseStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.FlagCarrierKills))?.Count ?? 0;
            player.StrongholdsCaptured = playerStat.FlexibleStats.MedalStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.StrongholdCaptured))?.Count ?? 0;
            player.StrongholdsSecured = playerStat.FlexibleStats.MedalStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.StrongholdSecured))?.Count ?? 0;
            player.StrongholdsDefended = playerStat.FlexibleStats.MedalStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.StrongholdDefense))?.Count ?? 0;
            player.Score = playerStat.FlexibleStats.ImpulseStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.Score))?.Count ?? 0;
            player.Kills = playerStat.FlexibleStats.ImpulseStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.Kills))?.Count ?? 0;
            player.RoundsSurvived = playerStat.FlexibleStats.ImpulseStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.RoundsSurvived))?.Count ?? 0;
            player.RoundsComplete = playerStat.FlexibleStats.ImpulseStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.RoundsComplete))?.Count ?? 0;
            player.BossTakedowns = playerStat.FlexibleStats.MedalStatCounts.Where(i => 
                i.Id == Guid.Parse(Constants.MythicTakedown) || 
                i.Id == Guid.Parse(Constants.LegendaryTakedown) ||
                i.Id == Guid.Parse(Constants.BossTakedown)).Sum(i=>i.Count);
            player.NpcKills = playerStat.EnemyKills.Sum(e=>e.TotalKills);
            player.BasesCaptured = playerStat.FlexibleStats.ImpulseStatCounts.FirstOrDefault(i => i.Id == Guid.Parse(Constants.BasesCaptured))?.Count ?? 0;


            List<GameEvent> killers = new List<GameEvent>();
            if (matchEvents != null)
            {
                killers = matchEvents.GameEvents.Where(e => e.VictimAgent == Enumeration.Agent.Player && 
                    e.Victim.Gamertag == player.GamerTag && 
                    e.KillerAgent == Enumeration.Agent.AI && (e.IsWeapon == null || e.IsWeapon == false) &&
                    e.EventName == Enumeration.EventType.Death).ToList();
            }

            foreach (var baseGroup in playerStat.EnemyKills.GroupBy(k=>k.Enemy.BaseId))
            {
                var enemyKill = baseGroup.First();
                Enemy enemy = new Enemy
                {
                    Id=enemyKill.Enemy.BaseId,
                    Name = enemyKill.Enemy.EnemyObject.Name,
                    Kills = baseGroup.Sum(k=>k.TotalKills),
                    Description = enemyKill.Enemy.EnemyObject.Description,
                    ImageUrl = enemyKill.Enemy.EnemyObject.SmallIconImageUrl,
                    Deaths = killers.Count(k=>k.KillerWeaponStockId == enemyKill.Enemy.BaseId),
                    IsPlayer = false
                };
                enemy.Spread = enemy.Kills - enemy.Deaths;
                player.Enemies.Add(enemy);
            }

            //foreach (var baseGroup in playerStat.DestroyedEnemyVehicles.GroupBy(k=>k.Enemy.BaseId))
            //{
            //    var enemyKill = baseGroup.First();
            //    Enemy enemy = new Enemy
            //    {
            //        Id = enemyKill.Enemy.BaseId,
            //        Name = enemyKill.Enemy.Vehicle.Name,
            //        Kills = baseGroup.Sum(k => k.TotalKills),
            //        Description = enemyKill.Enemy.Vehicle.Description,
            //        ImageUrl = enemyKill.Enemy.Vehicle.SmallIconImageUrl,
            //        Deaths = killers.Count(k => k.KillerWeaponStockId == enemyKill.Enemy.BaseId),
            //        IsPlayer = false
            //    };
            //    enemy.Spread = enemy.Kills - enemy.Deaths;
            //    player.Enemies.Add(enemy);
            //}

            var ids = playerStat.EnemyKills.Select(k => k.Enemy.BaseId)
                    .Union(playerStat.DestroyedEnemyVehicles.Select(v => v.Enemy.BaseId))
                    .ToList();
            killers.RemoveAll(k => ids.Contains(k.KillerWeaponStockId.Value));

            foreach (var killer in killers.Where(k=>k.KillerWeaponStockId.HasValue).GroupBy(k=>k.KillerWeaponStockId))
            {
                var enemyKill = await _metadataRepository.GetEnemyOrVehicle(killer.Key.Value);
                if (enemyKill != null)
                {
                    Enemy enemy = new Enemy
                    {
                        Id = enemyKill.Id,
                        Name = enemyKill.Name,
                        Kills = 0,
                        Description = enemyKill.Description,
                        ImageUrl = enemyKill.SmallIconImageUrl,
                        Deaths = killer.Count(),
                        IsPlayer = false
                    };
                    enemy.Spread = enemy.Kills - enemy.Deaths;
                    player.Enemies.Add(enemy);
                }
            }

            if (matchEvents != null)
            {
                foreach (var enemy in player.Enemies)
                {
                    var weaponKills = matchEvents.GameEvents.Where(e => e.EventName == Enumeration.EventType.Death &&
                        (e.VictimStockId == enemy.Id && e.Killer!=null && e.Killer.Gamertag == player.GamerTag) ||
                        e.Victim != null && e.Victim.Gamertag == player.GamerTag && e.KillerWeaponStockId == enemy.Id)
                        .Select(e=>new
                        {
                            Killer = e.Killer?.Gamertag,
                            //Victim = e.VictimStockId,
                            WeaponId = e.WeaponKillId(),
                            Type = e.SpartanKillType()
                        }).ToList();

                    var groups = weaponKills.Where(g=>g.WeaponId != 0).GroupBy(g => new { g.WeaponId, g.Type });
                    foreach (var g in groups)
                    {
                        var weapon = await _metadataRepository.GetWeaponOrEnemyOrVehicle(g.Key.WeaponId);
                        if (weapon != null)
                        {
                            var weaponKill = Mapper.Map<WeaponKill>(weapon);
                            weaponKill.Kills = g.Count(g2 => g2.Killer == player.GamerTag);
                            weaponKill.Deaths = g.Count(g2 => g2.Killer != player.GamerTag);
                            if (!string.IsNullOrEmpty(g.Key.Type))
                                weaponKill.Name = g.Key.Type;
                            enemy.WeaponKills.Add(weaponKill);
                        }
                        
                    }
                    enemy.WeaponKills = enemy.WeaponKills.OrderByDescending(e=>e.Kills).ThenBy(e=>e.Deaths).ToList();
                    //var weaponKills = matchEvents.GameEvents.Where(e => e.Killer != null && e.Victim != null &&
                    //        e.KillerWeaponStockId.HasValue && e.EventName == Enumeration.EventType.Death &&
                    //        ((e.Killer.Gamertag == tag && e.Victim.Gamertag == player.GamerTag) ||
                    //        (e.Killer.Gamertag == player.GamerTag && e.Victim.Gamertag == tag))).Select(e => new
                    //        {
                    //            Killer = e.Killer.Gamertag,
                    //            Victim = e.Victim.Gamertag,
                    //            WeaponId = e.KillerWeaponStockId.Value
                    //        }).ToList();
                }
            }
            
        }

        private void SetTeams(ITeamMatch match, List<ArenaPlayer> players, MatchResult result, string gamerTag)
        {
            result.Teams = new List<Team>();
            foreach (var teamStat in match.TeamStats.OrderBy(t => t.Rank))
            {
                var team = Mapper.Map<Team>(teamStat);
                team.Players = players.Where(p => p.TeamId == teamStat.TeamId).OrderBy(p => p.Rank).ToList();

                int minTotalKills = team.Players.Min(p => p.TotalKills);
                int maxTotalKills = team.Players.Max(p => p.TotalKills) - minTotalKills;

                int minDeaths = team.Players.Min(p => p.TotalDeaths);
                int maxDeaths = team.Players.Max(p => p.TotalDeaths) - minDeaths;

                int minAssists = team.Players.Min(p => p.TotalAssists);
                int maxAssists = team.Players.Max(p => p.TotalAssists) - minAssists;

                double maxKda = team.Players.Max(p => p.Kda);
                double minKda = team.Players.Min(p => p.Kda);

                int minHeadshots = team.Players.Min(p => p.TotalHeadshots);
                int maxHeadshots = team.Players.Max(p => p.TotalHeadshots) - minHeadshots;

                int minPerfectKill = team.Players.Min(p => p.TotalPerfectKills);
                int maxPerfectKill = team.Players.Max(p => p.TotalPerfectKills) - minPerfectKill;

                int minRoundWinningKills = team.Players.Min(p => p.RoundWinningKills);
                int maxRoundWinningKills = team.Players.Max(p => p.RoundWinningKills) - minRoundWinningKills;

                int minFlagCaptures = team.Players.Min(p => p.FlagCaptures);
                int maxFlagCaptures = team.Players.Max(p => p.FlagCaptures) - minFlagCaptures;
                int minFlagPulls = team.Players.Min(p => p.FlagPulls);
                int maxFlagPulls = team.Players.Max(p => p.FlagPulls) - minFlagPulls;
                int minFlagCarrierKills = team.Players.Min(p => p.FlagCarrierKills);
                int maxFlagCarrierKills = team.Players.Max(p => p.FlagCarrierKills) - minFlagCarrierKills;

                int minStrongHoldsCaptured = team.Players.Min(p => p.StrongholdsCaptured);
                int maxStrongHoldsCaptured = team.Players.Max(p => p.StrongholdsCaptured) - minStrongHoldsCaptured;
                int minStrongholdsSecured = team.Players.Min(p => p.StrongholdsSecured);
                int maxStrongholdsSecured = team.Players.Max(p => p.StrongholdsSecured) - minStrongholdsSecured;
                int minStrongholdsDefended = team.Players.Min(p => p.StrongholdsDefended);
                int maxStrongholdsDefended = team.Players.Max(p => p.StrongholdsDefended) - minStrongholdsDefended;

                int minScore = team.Players.Min(p => p.Score);
                int maxScore = team.Players.Max(p => p.Score) - minScore;
                int minKills = team.Players.Min(p => p.Kills);
                int maxKills = team.Players.Max(p => p.Kills) - minKills;
                int minRoundsSurvived = team.Players.Min(p => p.RoundsSurvived);
                int maxRoundsSurvived = team.Players.Max(p => p.RoundsSurvived) - minRoundsSurvived;
                int minRoundsComplete = team.Players.Min(p => p.RoundsComplete);
                int maxRoundsComplete = team.Players.Max(p => p.RoundsComplete) - minRoundsComplete;
                int minBossTakedowns = team.Players.Min(p => p.BossTakedowns);
                int maxBossTakedowns = team.Players.Max(p => p.BossTakedowns) - minBossTakedowns;

                int minTotalSpartanKills = team.Players.Min(p => p.TotalSpartanKills);
                int maxTotalSpartanKills = team.Players.Max(p => p.TotalSpartanKills) - minTotalSpartanKills;
                int minNpcKills = team.Players.Min(p => p.NpcKills);
                int maxNpcKills = team.Players.Max(p => p.NpcKills) - minNpcKills;

                foreach (var p in team.Players)
                {
                    p.TotalKillPercent = p.TotalKills.Percent(minTotalKills, maxTotalKills);
                    p.DeathPercent = p.TotalDeaths.Percent(minDeaths, maxDeaths);
                    p.AssistPercent = p.TotalAssists.Percent(minAssists, maxAssists);
                    p.KdaPercent = p.Kda.Percent(p.Kda < 0 ? minKda : maxKda);
                    p.HeadshotPercent = p.TotalHeadshots.Percent(minHeadshots, maxHeadshots);
                    p.RoundWinningKillsPercent = p.RoundWinningKills.Percent(minRoundWinningKills, maxRoundWinningKills);
                    p.PerfectKillPercent = p.TotalPerfectKills.Percent(minPerfectKill, maxPerfectKill);
                    p.FlagCapturePercent = p.FlagCaptures.Percent(minFlagCaptures,maxFlagCaptures);
                    p.FlagPullPercent = p.FlagPulls.Percent(minFlagPulls, maxFlagPulls);
                    p.FlagCarrierKillPercent = p.FlagCarrierKills.Percent(minFlagCarrierKills, maxFlagCarrierKills);
                    p.StrongholdCapturePercent = p.StrongholdsCaptured.Percent(minStrongHoldsCaptured, maxStrongHoldsCaptured);
                    p.StrongholdSecurePercent = p.StrongholdsSecured.Percent(minStrongholdsSecured, maxStrongholdsSecured);
                    p.StrongholdDefendPercent = p.StrongholdsDefended.Percent(minStrongholdsDefended, maxStrongholdsDefended);

                    p.ScorePercent = p.Score.Percent(minScore, maxScore);
                    p.KillsPercent = p.Kills.Percent(minKills, maxKills);
                    p.RoundsSurvivedPercent = p.RoundsSurvived.Percent(minRoundsSurvived, maxRoundsSurvived);
                    p.RoundsCompletePercent = p.RoundsComplete.Percent(minRoundsComplete, maxRoundsComplete);
                    p.BossTakedownsPercent = p.BossTakedowns.Percent(minBossTakedowns, maxBossTakedowns);

                    p.TotalSpartanKillPercent = p.TotalSpartanKills.Percent(minTotalSpartanKills, maxTotalSpartanKills);
                    p.NpcKillPercent = p.NpcKills.Percent(minNpcKills, maxNpcKills);
                }

                result.Teams.Add(team);
            }

            var player = players.FirstOrDefault(p => p.GamerTag.ToUpper() == gamerTag.ToUpper());
            var playersTeam = result.Teams.FirstOrDefault(t => t.TeamId == player?.TeamId);
            if (playersTeam != null && player != null)
            {
                player.IsPlayer = true;
                if (player.DNF)
                {
                    result.Result = Enumeration.ResultType.DidNotFinish;
                }
                else if (playersTeam.Rank == 1)
                {
                    result.Result = result.Teams.Any(t => t.TeamId != playersTeam.TeamId && t.Rank == 0) ? Enumeration.ResultType.Tied : Enumeration.ResultType.Won;
                }
                else
                {
                    result.Result = Enumeration.ResultType.Lost;
                }
                result.PlayerTeamColor = playersTeam.TeamColor;
            }
        }
    }
}
