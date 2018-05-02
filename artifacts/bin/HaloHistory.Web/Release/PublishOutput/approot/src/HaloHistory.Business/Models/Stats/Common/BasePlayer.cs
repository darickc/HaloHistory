using System;
using System.Collections.Generic;
using HaloSharp.Model;

namespace HaloHistory.Business.Models.Stats.Common
{
    public class BasePlayer// : BasePlayerStat
    {
        public TimeSpan AvgLifeTimeOfPlayer { get; set; }
        public bool DNF { get; set; }
        public string GamerTag { get; set; }
        public int Rank { get; set; }
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
        public double TotalPowerWeaponDamage { get; set; }
        public int TotalPowerWeaponGrabs { get; set; }
        public int TotalPowerWeaponKills { get; set; }
        public TimeSpan TotalPowerWeaponPossessionTime { get; set; }
        public int TotalShotsFired { get; set; }
        public int TotalShotsLanded { get; set; }
        public double TotalShoulderBashDamage { get; set; }
        public int TotalShoulderBashKills { get; set; }
        public int TotalSpartanKills { get; set; }
        public TimeSpan TotalTimePlayed { get; set; }
        public double TotalWeaponDamage { get; set; }

        public int KillPercent { get; set; }
        public int DeathPercent { get; set; }
        public int AssistPercent { get; set; }
        public int KdaPercent { get; set; }

        public double Kda
        {
            get
            {
                double games = TotalGamesCompleted > 0 ? (double)TotalGamesCompleted : 1d;
                return Math.Round((((double)TotalKills + ((double)TotalAssists)/3) - (double)TotalDeaths)/ games, 1);

            }
        }

        public string SpartanImageUri { get; set; }
        public string EmblemImageUri { get; set; }

        public List<WeaponStat> Weapons { get; set; }

        public List<MedalClassification> Medals { get; set; }
    }
}
