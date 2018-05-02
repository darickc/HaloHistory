using System;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class PlaylistData : BaseDataEntity<string, Playlist>
    {
        public PlaylistData()
        {
        }

        public PlaylistData(Guid id, Playlist data) : base(id.ToString(), data)
        {
        }
    }
}
