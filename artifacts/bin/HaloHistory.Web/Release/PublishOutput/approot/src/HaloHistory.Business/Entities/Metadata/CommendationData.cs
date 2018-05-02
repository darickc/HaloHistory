using System;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class CommendationData : BaseDataEntity<string, Commendation>
    {
        public CommendationData()
        {
        }

        public CommendationData(Guid id, Commendation data) : base(id.ToString(), data)
        {
        }
    }
}
