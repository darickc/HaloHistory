using System.Threading.Tasks;
using HaloHistory.Business;
using HaloHistory.Business.Entities;
using HaloHistory.Business.Models.Profile;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HaloHistory.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private IProfileBusiness ProfileBusiness { get; }
        private ICartographerContext _context { get; }
        public ProfileController(IProfileBusiness profileBusiness, ICartographerContext context)
        {
            ProfileBusiness = profileBusiness;
            _context = context;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<PlayerProfile> Get(string id)
        {
            var player = await ProfileBusiness.GetPlayer(id);
            await _context.CommitChanges();
            return player;
        }

        
    }
}
