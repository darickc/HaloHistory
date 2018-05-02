using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public StatsBusiness(IStatsRepository statsRepository, IMetadataRepository metadataRepository)
        {
            _statsRepository = statsRepository;
            _metadataRepository = metadataRepository;
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

            return matches;
        }

        public async Task<BaseMatchResult> GetMatch(Enumeration.GameMode gameMode, Guid id, string gamerTag)
        {
            BaseMatchResult match = null;
            
            switch (gameMode)
            {
                case Enumeration.GameMode.Arena:
                    var arenaMatch = await _statsRepository.GetArenaMatch(id);
                    match = await GetMatchResult(arenaMatch, gamerTag);
                    break;
                case Enumeration.GameMode.Campaign:
                    var campaignMatch = await _statsRepository.GetCampaignMatch(id);
                    match = GetMatchResult(campaignMatch, gamerTag);
                    break;
                case Enumeration.GameMode.Custom:
                    var customMatch = await _statsRepository.GetCustomMatch(id);
                    match = await GetMatchResult(customMatch, gamerTag);
                    break;
                case Enumeration.GameMode.Warzone:
                    MatchEvents matchEvents = await _statsRepository.GetEventsForMatch(id);
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
            return match;
        }


        private async Task<ArenaMatchResult> GetMatchResult(ArenaMatch match, string gamerTag)
        {
            ArenaMatchResult result = Mapper.Map<ArenaMatchResult>(match);
            var players = new List<ArenaPlayer>();
            foreach (var playerStat in match.PlayerStats)
            {
                var p = await GetArenaPlayer(playerStat);
                players.Add(p);
            }
            SetTeams(match, players, result,gamerTag);

            //result.Teams = new List<Team>();
            //foreach (var teamStat in match.TeamStats.OrderBy(t => t.Rank))
            //{
            //    var team = Mapper.Map<Team>(teamStat);
            //    team.Players = players.Where(p => p.TeamId == teamStat.TeamId).OrderBy(p => p.Rank).ToList();

            //    var minKills = team.Players.Min(p => p.TotalKills);
            //    var maxKills = team.Players.Max(p => p.TotalKills) - minKills;
            //    var minDeaths = team.Players.Min(p => p.TotalDeaths);
            //    var maxDeaths = team.Players.Max(p => p.TotalDeaths) - minDeaths;
            //    var minAssists = team.Players.Min(p => p.TotalAssists);
            //    var maxAssists = team.Players.Max(p => p.TotalAssists) - minAssists;
            //    var maxKda = team.Players.Max(p =>p.Kda);
            //    var minKda = team.Players.Min(p => p.Kda);

            //    foreach (var p in team.Players)
            //    {
            //        p.KillPercent = p.TotalKills.Percent(minKills,maxKills);
            //        p.DeathPercent = p.TotalDeaths.Percent(minDeaths, maxDeaths);
            //        p.AssistPercent = p.TotalAssists.Percent(minAssists,maxAssists);
            //        p.KdaPercent = p.Kda.Percent(p.Kda < 0 ? minKda : maxKda);
            //    }

            //    result.Teams.Add(team);
            //}

            //var player = players.FirstOrDefault(p => p.GamerTag.ToUpper() == gamerTag.ToUpper());
            //var playersTeam = result.Teams.FirstOrDefault(t => t.TeamId == player?.TeamId);
            //if (playersTeam != null && player != null)
            //{
            //    if (player.DNF)
            //    {
            //        result.Result = Enumeration.ResultType.DidNotFinish;
            //    }
            //    else if (playersTeam.Rank == 0)
            //    {
            //        result.Result = result.Teams.Any(t => t.TeamId != playersTeam.TeamId && t.Rank == 0) ? Enumeration.ResultType.Tied : Enumeration.ResultType.Won;
            //    }
            //    else
            //    {
            //        result.Result = Enumeration.ResultType.Lost;
            //    }
            //    result.PlayerTeamColor = playersTeam.TeamColor;
            //}


            return result;
        }

        private async Task<ArenaMatchResult> GetMatchResult(CustomMatch match, string gamerTag)
        {
            ArenaMatchResult result = Mapper.Map<ArenaMatchResult>(match);
            var players = new List<ArenaPlayer>();
            foreach (var playerStat in match.PlayerStats)
            {
                var p = await GetCustomPlayer(playerStat);
                players.Add(p);
            }
            SetTeams(match, players, result, gamerTag);
            return result;
        }

        private async Task<ArenaMatchResult> GetMatchResult(WarzoneMatch match, string gamerTag, MatchEvents matchEvents)
        {
            ArenaMatchResult result = Mapper.Map<ArenaMatchResult>(match);
            var players = new List<ArenaPlayer>();
            foreach (var playerStat in match.PlayerStats)
            {
                var p = await GetWarzonePlayer(playerStat, matchEvents);
                players.Add(p);
            }

            SetTeams(match, players, result, gamerTag);



            return result;
        }

        private ArenaMatchResult GetMatchResult(CampaignMatch match, string gamerTag)
        {
            ArenaMatchResult result = Mapper.Map<ArenaMatchResult>(match);
            //var players = match.PlayerStats.Select(GetCampaignPlayer).ToList();
            return result;
        }

        private async Task<ArenaPlayer> GetArenaPlayer(ArenaMatchPlayerStat playerStat)
        {
            ArenaPlayer player = Mapper.Map<ArenaPlayer>(playerStat);
            await SetCompetetivePlayer(playerStat, player);


            player.CurrentCsr = new CompetitiveSkillRanking
            {
                Name = playerStat.CurrentCsr?.Designation?.Name,
                BannerImageUrl = playerStat.CurrentCsr?.Designation?.BannerImageUrl,
                Tier = playerStat.CurrentCsr?.Tier,
                IconImageUrl = playerStat.CurrentCsr?.CurrentTier?.IconImageUrl,
                PercentToNextTier = playerStat.CurrentCsr?.PercentToNextTier,
                Rank = playerStat.CurrentCsr?.Rank,
                DesignationId = (int?)playerStat.CurrentCsr?.DesignationId ?? 0
            };

            player.PreviousCsr = new CompetitiveSkillRanking
            {
                Name = playerStat.PreviousCsr?.Designation?.Name,
                BannerImageUrl = playerStat.PreviousCsr?.Designation?.BannerImageUrl,
                Tier = playerStat.PreviousCsr?.Tier,
                IconImageUrl = playerStat.PreviousCsr?.CurrentTier?.IconImageUrl,
                PercentToNextTier = playerStat.PreviousCsr?.PercentToNextTier,
                Rank = playerStat.PreviousCsr?.Rank,
                DesignationId = (int?)playerStat.PreviousCsr?.DesignationId ?? 0
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

        private async Task<ArenaPlayer> GetCustomPlayer(CustomMatchPlayerStat playerStat)
        {
            ArenaPlayer player = Mapper.Map<ArenaPlayer>(playerStat);
            await SetPlayer(playerStat,player);
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
                player.Enemies.Add(enemy);
            }

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

            player.Enemies = player.Enemies.OrderByDescending(e => e.Kills).ThenBy(e => e.Deaths).ToList();
        }

        private async Task SetBasePlayer(IBasePlayerStat playerStat, ArenaPlayer player, MatchEvents matchEvents = null)
        {
            player.Weapons = playerStat.WeaponStats.Where(w => w.Weapon != null && (w.TotalDamageDealt > 0 || w.TotalShotsFired > 0)).OrderByDescending(w => w.TotalKills).Select(Mapper.Map<WeaponStat>).ToList();

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

            player.Medals = playerStat.MedalAwards.GroupBy(m => m.Medal.Classification).Select(m =>
                new MedalClassification
                {
                    Classification = m.Key,
                    MedalAwards = Mapper.Map<List<MedalAward>>(m)
                }).ToList();

            List<GameEvent> killers = new List<GameEvent>();
            if (matchEvents != null)
            {
                killers = matchEvents.GameEvents.Where(e => e.VictimAgent == Enumeration.Agent.Player && 
                    e.Victim.Gamertag == player.GamerTag && 
                    e.KillerAgent == Enumeration.Agent.AI && !e.IsWeapon &&
                    e.EventName == Enumeration.EventType.Death).ToList();
            }

            foreach (var baseGroup in playerStat.EnemyKills.GroupBy(k=>k.Enemy.BaseId))
            {
                var enemyKill = baseGroup.First();
                Enemy enemy = new Enemy
                {
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

            foreach (var baseGroup in playerStat.DestroyedEnemyVehicles.GroupBy(k=>k.Enemy.BaseId))
            {
                var enemyKill = baseGroup.First();
                Enemy enemy = new Enemy
                {
                    Name = enemyKill.Enemy.Vehicle.Name,
                    Kills = baseGroup.Sum(k => k.TotalKills),
                    Description = enemyKill.Enemy.Vehicle.Description,
                    ImageUrl = enemyKill.Enemy.Vehicle.SmallIconImageUrl,
                    Deaths = killers.Count(k => k.KillerWeaponStockId == enemyKill.Enemy.BaseId),
                    IsPlayer = false
                };
                enemy.Spread = enemy.Kills - enemy.Deaths;
                player.Enemies.Add(enemy);
            }

            var ids = playerStat.EnemyKills.Select(k => k.Enemy.BaseId)
                    .Union(playerStat.DestroyedEnemyVehicles.Select(v => v.Enemy.BaseId))
                    .ToList();
            killers.RemoveAll(k => ids.Contains(k.KillerWeaponStockId));

            foreach (var killer in killers.GroupBy(k=>k.KillerWeaponStockId))
            {
                var enemyKill = await _metadataRepository.GetEnemyOrVehicl(killer.Key);
                if (enemyKill != null)
                {
                    Enemy enemy = new Enemy
                    {
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
        }

        private void SetTeams(ITeamMatch match, List<ArenaPlayer> players, ArenaMatchResult result, string gamerTag)
        {
            result.Teams = new List<Team>();
            foreach (var teamStat in match.TeamStats.OrderBy(t => t.Rank))
            {
                var team = Mapper.Map<Team>(teamStat);
                team.Players = players.Where(p => p.TeamId == teamStat.TeamId).OrderBy(p => p.Rank).ToList();

                var minKills = team.Players.Min(p => p.TotalKills);
                var maxKills = team.Players.Max(p => p.TotalKills) - minKills;
                var minDeaths = team.Players.Min(p => p.TotalDeaths);
                var maxDeaths = team.Players.Max(p => p.TotalDeaths) - minDeaths;
                var minAssists = team.Players.Min(p => p.TotalAssists);
                var maxAssists = team.Players.Max(p => p.TotalAssists) - minAssists;
                var maxKda = team.Players.Max(p => p.Kda);
                var minKda = team.Players.Min(p => p.Kda);

                foreach (var p in team.Players)
                {
                    p.KillPercent = p.TotalKills.Percent(minKills, maxKills);
                    p.DeathPercent = p.TotalDeaths.Percent(minDeaths, maxDeaths);
                    p.AssistPercent = p.TotalAssists.Percent(minAssists, maxAssists);
                    p.KdaPercent = p.Kda.Percent(p.Kda < 0 ? minKda : maxKda);
                }

                result.Teams.Add(team);
            }

            var player = players.FirstOrDefault(p => p.GamerTag.ToUpper() == gamerTag.ToUpper());
            var playersTeam = result.Teams.FirstOrDefault(t => t.TeamId == player?.TeamId);
            if (playersTeam != null && player != null)
            {
                if (player.DNF)
                {
                    result.Result = Enumeration.ResultType.DidNotFinish;
                }
                else if (playersTeam.Rank == 0)
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
