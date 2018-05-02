﻿using System;
using HaloSharp.Model.Stats.Common;
using Newtonsoft.Json;

namespace HaloSharp.Model.Stats.Lifetime.Common
{
    //[Serializable]
    public class GameBaseVariantStat : BaseStat, IEquatable<GameBaseVariantStat>
    {
        /// <summary>
        /// The game base variant specific stats. Flexible stats are available via the Metadata API.
        /// </summary>
        [JsonProperty(PropertyName = "FlexibleStats")]
        public FlexibleStats FlexibleStats { get; set; }

        /// <summary>
        /// The ID of the game base variant. Game base variants are available via the Metadata API.
        /// </summary>
        [JsonProperty(PropertyName = "GameBaseVariantId")]
        public Guid GameBaseVariantId { get; set; }

        public bool Equals(GameBaseVariantStat other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return base.Equals(other)
                && Equals(FlexibleStats, other.FlexibleStats)
                && GameBaseVariantId.Equals(other.GameBaseVariantId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof (GameBaseVariantStat))
            {
                return false;
            }

            return Equals((GameBaseVariantStat) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (FlexibleStats?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ GameBaseVariantId.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(GameBaseVariantStat left, GameBaseVariantStat right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GameBaseVariantStat left, GameBaseVariantStat right)
        {
            return !Equals(left, right);
        }
    }
}