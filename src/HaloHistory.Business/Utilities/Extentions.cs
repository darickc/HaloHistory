//using System.Threading.Tasks;
//using SQLite.Net.Async;

using System;
using HaloSharp.Model;
using HaloSharp.Model.Stats.CarnageReport;

namespace HaloHistory.Business.Utilities
{
    internal static class Extentions
    {
        //public static async Task<int> Count<T>(this SQLiteAsyncConnection db)
        //{
        //    var count = await db.ExecuteScalarAsync<int>($"select count(id) from {typeof(T).Name}");
        //    return count;
        //}

        public static int Percent(this int a, int b)
        {
            if (b == 0)
                return 0;
            int percent = (int)((double)a/ (double)b * 100);
            return percent;
        }

        public static int Percent(this int value, int low, int high)
        {
            if (high == 0)
                return 0;
            value -= low;
            int percent = (int)((double)value / (double)high * 100);
            return percent;
        }

        public static int Percent(this double a, double b)
        {
            if (b == 0)
            {
                return 0;
            }
            double percent = Math.Abs(a) / Math.Abs(b) * 100;
            return (int)percent;
        }

        public static int Percent(this double value, double low, double high)
        {
            if (high == 0)
            {
                return 0;
            }
            value = Math.Abs(value);
            value -= Math.Abs(low);
            double percent = value / Math.Abs(high) * 100;
            return (int)percent;
        }

        public static long WeaponKillId(this GameEvent gameEvent)
        {
            if (gameEvent.KillerAgent == Enumeration.Agent.Player && gameEvent.IsWeapon == false)
                return Constants.SpartanWeaponId;
            return gameEvent.KillerWeaponStockId ?? 0;
        }

        public static string SpartanKillType(this GameEvent gameEvent)
        {
            if (gameEvent.IsWeapon == true)
                return null;
            if (gameEvent.IsAssassination == true)
                return "Assassination";
            if (gameEvent.IsGroundPound == true)
                return "Ground Pound";
            if (gameEvent.IsShoulderBash == true)
                return "Shoulder Bash";

            return "Melee";
        }

    }
}
