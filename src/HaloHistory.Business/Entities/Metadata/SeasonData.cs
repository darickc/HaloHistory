using System;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class SeasonData : BaseDataEntity<string, Season>
    {
        public SeasonData()
        {
        }

        public SeasonData(Guid id, Season data) : base(id.ToString(), data)
        {
        }
    }
}
