using System;
using System.Collections.Generic;

namespace HaloSharp.Model.Stats.Common
{
    public interface IBaseStat
    {
        List<EnemySet> DestroyedEnemyVehicles { get; set; }
        List<EnemySet> EnemyKills { get; set; }
        List<Impulse> Impulses { get; set; }
        List<MedalAward> MedalAwards { get; set; }
        int TotalAssassinations { get; set; }
        int TotalAssists { get; set; }
        int TotalDeaths { get; set; }
        int TotalGamesCompleted { get; set; }
        int TotalGamesLost { get; set; }
        int TotalGamesTied { get; set; }
        int TotalGamesWon { get; set; }
        double TotalGrenadeDamage { get; set; }
        int TotalGrenadeKills { get; set; }
        double TotalGroundPoundDamage { get; set; }
        int TotalGroundPoundKills { get; set; }
        int TotalHeadshots { get; set; }
        int TotalKills { get; set; }
        double TotalMeleeDamage { get; set; }
        int TotalMeleeKills { get; set; }
        double TotalPowerWeaponDamage { get; set; }
        int TotalPowerWeaponGrabs { get; set; }
        int TotalPowerWeaponKills { get; set; }
        TimeSpan TotalPowerWeaponPossessionTime { get; set; }
        int TotalShotsFired { get; set; }
        int TotalShotsLanded { get; set; }
        double TotalShoulderBashDamage { get; set; }
        int TotalShoulderBashKills { get; set; }
        int TotalSpartanKills { get; set; }
        TimeSpan TotalTimePlayed { get; set; }
        double TotalWeaponDamage { get; set; }
        List<WeaponStat> WeaponStats { get; set; }
        WeaponStat WeaponWithMostKills { get; set; }
    }
}