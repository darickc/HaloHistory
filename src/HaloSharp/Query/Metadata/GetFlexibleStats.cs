﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HaloSharp.Model.Metadata;

namespace HaloSharp.Query.Metadata
{
    /// <summary>
    ///     Construct a query to retrieve detailed Flexible Stat Metadata. Use them to translate IDs from other APIs.
    /// </summary>
    public class GetFlexibleStats : IQuery<List<FlexibleStat>>
    {
        private bool _useCache = true;

        public GetFlexibleStats SkipCache()
        {
            _useCache = false;

            return this;
        }

        public async Task<List<FlexibleStat>> ApplyTo(IHaloSession session)
        {
            var uri = GetConstructedUri();

            var flexibleStats = _useCache
                ? Cache.Get<List<FlexibleStat>>(uri)
                : null;

            if (flexibleStats == null)
            {
                flexibleStats = await session.Get<List<FlexibleStat>>(uri);

                Cache.AddMetadata(uri, flexibleStats);
            }

            return flexibleStats;
        }

        public string GetConstructedUri()
        {
            var builder = new StringBuilder("metadata/h5/metadata/flexible-stats");

            return builder.ToString();
        }
    }
}