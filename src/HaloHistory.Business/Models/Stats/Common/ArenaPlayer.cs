using System;
using System.Collections.Generic;
using HaloSharp.Model.Stats.CarnageReport.Common;

namespace HaloHistory.Business.Models.Stats.Common
{
    public class ArenaPlayer
    {
        public bool IsPlayer { get; set; }
        public TimeSpan AvgLifeTimeOfPlayer { get; set; }
        public bool DNF { get; set; }

        public int FlagCaptures { get; set; }
        public int FlagPulls { get; set; }
        public int FlagCarrierKills { get; set; }
        public int Score { get; set; }
        public int ScorePercent { get; set; }
        public int Kills { get; set; }
        public int KillsPercent { get; set; }
        public int RoundsSurvived { get; set; }
        public int RoundsSurvivedPercent { get; set; }
        public int RoundsComplete { get; set; }
        public int RoundsCompletePercent { get; set; }
        public int BossTakedowns { get; set; }
        public int BossTakedownsPercent { get; set; }
        public int BasesCaptured { get; set; }
        public int BasesCapturedPercent { get; set; }
        public string GamerTag { get; set; }
        public int Rank { get; set; }
        public int RoundWinningKills { get; set; }
        public int RoundWinningKillsPercent { get; set; }
        public int StrongholdsCaptured { get; set; }
        public int StrongholdsSecured { get; set; }
        public int StrongholdsDefended { get; set; }
        public int TeamId { get; set; }

        public int TotalAssassinations { get; set; }
        public int TotalAssists { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalGamesCompleted { get; set; }
        public int TotalGamesLost { get; set; }
        public int TotalGamesTied { get; set; }
        public int TotalGamesWon { get; set; }
        public double TotalGrenadeDamage { get; set; }
        public int TotalGrenadeKills { get; set; }
        public double TotalGroundPoundDamage { get; set; }
        public int TotalGroundPoundKills { get; set; }
        public int TotalHeadshots { get; set; }
        public int TotalKills { get; set; }
        public double TotalMeleeDamage { get; set; }
        public int TotalMeleeKills { get; set; }
        public int TotalPerfectKills { get; set; }
        public double TotalPowerWeaponDamage { get; set; }
        public int TotalPowerWeaponGrabs { get; set; }
        public int TotalPowerWeaponKills { get; set; }
        public TimeSpan TotalPowerWeaponPossessionTime { get; set; }
        public int TotalShotsFired { get; set; }
        public int TotalShotsLanded { get; set; }
        public double TotalShoulderBashDamage { get; set; }
        public int TotalShoulderBashKills { get; set; }
        public int TotalSpartanKills { get; set; }
        public int TotalSpartanKillPercent { get; set; }
        public int NpcKills { get; set; }
        public int NpcKillPercent { get; set; }
        public TimeSpan TotalTimePlayed { get; set; }
        public double TotalWeaponDamage { get; set; }

        public int TotalKillPercent { get; set; }
        public int DeathPercent { get; set; }
        public int AssistPercent { get; set; }
        public int KdaPercent { get; set; }
        public int HeadshotPercent { get; set; }
        public int PerfectKillPercent { get; set; }
        public int FlagCapturePercent { get; set; }
        public int FlagPullPercent { get; set; }
        public int FlagCarrierKillPercent { get; set; }
        public int StrongholdCapturePercent { get; set; }
        public int StrongholdSecurePercent { get; set; }
        public int StrongholdDefendPercent { get; set; }

        public double Kda
        {
            get
            {
                double games = TotalGamesCompleted > 0 ? (double)TotalGamesCompleted : 1d;
                return Math.Round((((double)TotalKills + ((double)TotalAssists) / 3) - (double)TotalDeaths) / games, 1);

            }
        }

        public string SpartanImageUri { get; set; }
        public string EmblemImageUri { get; set; }

        public List<WeaponStat> Weapons { get; set; }

        public List<MedalClassification> Medals { get; set; }

        public CreditsEarned CreditsEarned { get; set; }
        public CompetitiveSkillRanking CurrentCsr { get; set; }
        public CompetitiveSkillRanking PreviousCsr { get; set; }
        public List<Enemy> Enemies { get; set; }
        
        public int MeasurementMatchesLeft { get; set; }
        public XpInfo XpInfo { get; set; }

        public ArenaPlayer()
        {
            Enemies = new List<Enemy>();
        }
    }
}
