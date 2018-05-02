using System;
using System.Threading.Tasks;
using HaloHistory.Business.Entities;
using HaloHistory.Business.Entities.Profile;
using HaloHistory.Business.Enums;
using HaloSharp;
using HaloSharp.Extension;
using HaloSharp.Model;
using HaloSharp.Query.Profile;

namespace HaloHistory.Business.Repositories.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ICartographerContext _db;
        private readonly IHaloSession _session;

        public ProfileRepository(ICartographerContext context, IHaloSession session)
        {
            _session = session;
            _db = context;
        }

        public void Initialize()
        {
        }

        public async Task<string> GetEmplemImageUrl(string gamertag, ImageSize size = ImageSize.ExtraSmall)
        {
            gamertag = gamertag.ToUpper();
            var data = await _db.FindAsync<EmblemImageData>(gamertag);
            if (data == null || (DateTime.UtcNow - data.Timestamp.ToUniversalTime()).TotalDays > 3)
            {
                var query = new GetEmblemImage().ForPlayer(gamertag).Size((int) size);
                var image = await _session.Query(query);
                if (image == null)
                    return null;

                if (data == null)
                {
                    data = new EmblemImageData(gamertag, image.Uri, DateTime.UtcNow);
                    _db.InsertAsync(data);
                }
                else
                {
                    data.Data = image.Uri;
                    data.Timestamp = DateTime.UtcNow;
                    //await _db.UpdateAsync(data);
                }
            }

            return data.Data;
        }

        public async Task<string> GetSpartanImageUrl(string gamertag, ImageSize size = ImageSize.ExtraSmall, Enumeration.CropType crop = Enumeration.CropType.Full)
        {
            gamertag = gamertag.ToUpper();
            var data = await _db.FindAsync<SpartanImageData>(gamertag);
            if (data == null || (DateTime.UtcNow - data.Timestamp.ToUniversalTime()).TotalDays > 3)
            {
                var query = new GetSpartanImage().ForPlayer(gamertag).Size((int)size).Crop(crop);
                var image = await _session.Query(query);
                if (image == null)
                    return null;

                if (data == null)
                {
                    data = new SpartanImageData(gamertag, image.Uri, DateTime.UtcNow);
                    _db.InsertAsync(data);
                }
                else
                {
                    data.Data = image.Uri;
                    data.Timestamp = DateTime.UtcNow;
                    //await _db.UpdateAsync(data);
                }

            }

            return data.Data;
        }
    }
}
