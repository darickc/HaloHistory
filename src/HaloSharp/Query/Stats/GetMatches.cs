﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaloSharp.Exception;
using HaloSharp.Model;
using HaloSharp.Model.Stats;
using HaloSharp.Validation.Stats;

namespace HaloSharp.Query.Stats
{
    /// <summary>
    ///     Construct a query to retrieve a list of matches that the player has participated in and which have completed
    ///     processing. If the player is currently in a match, it is not returned in this API. Matches will usually appear
    ///     in this list within a minute of the match ending.
    /// </summary>
    public class GetMatches : IQuery<MatchSet>
    {
        internal readonly IDictionary<string, string> Parameters = new Dictionary<string, string>();
        internal string Player;

        private bool _useCache = true;

        public GetMatches SkipCache()
        {
            _useCache = false;

            return this;
        }

        /// <summary>
        ///     The Player's gamertag.
        /// </summary>
        /// <param name="gamertag">The Player's gamertag.</param>
        public GetMatches ForPlayer(string gamertag)
        {
            Player = gamertag;

            return this;
        }

        /// <summary>
        ///     Indicates what game mode the client is interested in getting matches for (arena, campaign, custom, or
        ///     warzone).
        /// </summary>
        /// <param name="gameMode">The Game Mode the client is interested in.</param>
        public GetMatches InGameMode(Enumeration.GameMode gameMode)
        {
            Parameters["modes"] = gameMode.ToString();

            return this;
        }

        /// <summary>
        ///     Indicates what game modes the client is interested in getting matches for (arena, campaign, custom, or
        ///     warzone).
        /// </summary>
        /// <param name="gameModes">The Game Mode(s) the client is interested in.</param>
        public GetMatches InGameModes(List<Enumeration.GameMode> gameModes)
        {
            Parameters["modes"] = string.Join(",", gameModes.Select(g => g.ToString()));

            return this;
        }

        /// <summary>
        ///     When specified, this indicates the starting index (0-based) for which the batch of results will begin at.
        /// </summary>
        /// <param name="count">The starting index (0-based) for which the batch of results will begin at.</param>
        public GetMatches Skip(int count)
        {
            Parameters["start"] = count.ToString();

            return this;
        }

        /// <summary>
        ///     When specified, this indicates the maximum quantity of items the client would like returned in the
        ///     response. When the value is greater than the allowed range [1,25], the maximum allowed value is used
        ///     instead. The "Count" field in the response will confirm the actual value that was used.
        /// </summary>
        /// <param name="count">The maximum quantity of items the client would like returned.</param>
        public GetMatches Take(int count)
        {
            Parameters["count"] = count.ToString();

            return this;
        }

        public async Task<MatchSet> ApplyTo(IHaloSession session)
        {
            this.Validate();

            var uri = GetConstructedUri();

            var matchSet = _useCache
                ? Cache.Get<MatchSet>(uri)
                : null;

            if (matchSet == null)
            {
                matchSet = await session.Get<MatchSet>(uri);

                Cache.AddStats(uri, matchSet);
            }

            return matchSet;
        }

        public string GetConstructedUri()
        {
            var builder = new StringBuilder($"stats/h5/players/{Player}/matches");

            if (Parameters.Any())
            {
                builder.Append("?");
                builder.Append(string.Join("&", Parameters.Select(p => $"{p.Key}={p.Value}")));
            }

            return builder.ToString();
        }
    }
}