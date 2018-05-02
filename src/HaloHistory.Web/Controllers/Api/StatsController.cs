using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HaloHistory.Business;
using HaloHistory.Business.Models.Stats;
using HaloSharp.Model;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HaloHistory.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class StatsController : Controller
    {
        private IStatsBusiness StatsBusiness { get; }

        public StatsController(IStatsBusiness statsBusiness)
        {
            StatsBusiness = statsBusiness;
        }

        // GET api/values/5
        [HttpGet("matches/{gamerTag}/{gameMode?}")]
        public async Task<MatchSet> Get(string gamerTag, Enumeration.GameMode? gameMode,[FromQuery] int start = 0)
        {
            MatchSet matches;
            if (gameMode.HasValue)
            {
                matches = await StatsBusiness.GetMatchesForPlayer(gamerTag, start, gameMode.Value);
            }
            else
            {
                matches = await StatsBusiness.GetMatchesForPlayer(gamerTag, start, Enumeration.GameMode.Arena, Enumeration.GameMode.Custom, Enumeration.GameMode.Warzone);
            }
            return matches;
        }

        [HttpGet("matches/{gamerTag}/{gameMode}/{id}")]
        public async Task<IActionResult> GetMatch(string gamerTag, Enumeration.GameMode gameMode, Guid id)
        {
            var match = await StatsBusiness.GetMatch(gameMode, id, gamerTag);
            return Content(match, "application/json");
        }
    }
}
