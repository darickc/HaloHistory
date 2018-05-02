using System.Threading.Tasks;
using HaloHistory.Business.Models.Profile;

namespace HaloHistory.Business
{
    public interface IProfileBusiness
    {
        Task<PlayerProfile> GetPlayer(string gamertag);
    }
}