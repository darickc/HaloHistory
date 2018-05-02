using System;
using System.Threading.Tasks;
using HaloHistory.Business.Models.Profile;
using HaloHistory.Business.Repositories.Profile;

namespace HaloHistory.Business
{
    public class ProfileBusiness : IProfileBusiness
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileBusiness(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<PlayerProfile> GetPlayer(string gamertag)
        {
            try
            {
                var profile = new PlayerProfile
                {
                    Gamertag = gamertag,
                    EmblemImageUri = await _profileRepository.GetEmplemImageUrl(gamertag)
                };

                if (string.IsNullOrEmpty(profile.EmblemImageUri))
                    return null;

                profile.SpartanImageUri = await _profileRepository.GetSpartanImageUrl(gamertag);
                return profile;

            }
            catch (Exception ex)
            {

                return null;
            }

        }
    }
}
