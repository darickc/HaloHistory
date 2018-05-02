using System;
using System.Threading.Tasks;
using HaloHistory.Business;
using HaloHistory.Business.Entities;
using HaloHistory.Business.Models.Stats;
using HaloSharp.Model;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HaloHistory.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class StatsController : Controller
    {
        private IStatsBusiness StatsBusiness { get; }
        private ICartographerContext Context { get; }

        public StatsController(IStatsBusiness statsBusiness, ICartographerContext context)
        {
            StatsBusiness = statsBusiness;
            Context = context;
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
                matches = await StatsBusiness.GetMatchesForPlayer(gamerTag, start);
            }
            await Context.CommitChanges();
            return matches;
        }

        [HttpGet("matches/{gamerTag}/{gameMode}/{id}")]
        public async Task<BaseMatchResult> GetMatch(string gamerTag, Enumeration.GameMode gameMode, Guid id)
        {
            var match = await StatsBusiness.GetMatch(gameMode, id, gamerTag);
            await Context.CommitChanges();
            return match;
        }
    }
}
