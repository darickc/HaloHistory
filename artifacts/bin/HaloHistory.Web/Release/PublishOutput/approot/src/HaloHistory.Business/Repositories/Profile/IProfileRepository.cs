using System.Threading.Tasks;
using HaloHistory.Business.Enums;
using HaloSharp.Model;

namespace HaloHistory.Business.Repositories.Profile
{
    public interface IProfileRepository
    {
        Task<string> GetEmplemImageUrl(string gamertag, ImageSize size = ImageSize.ExtraSmall);
        Task<string> GetSpartanImageUrl(string gamertag, ImageSize size = ImageSize.Small, Enumeration.CropType crop = Enumeration.CropType.Full);
        void Initialize();
    }
}