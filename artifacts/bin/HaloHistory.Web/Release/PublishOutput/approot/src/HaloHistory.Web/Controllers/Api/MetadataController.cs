using System.Threading.Tasks;
using HaloHistory.Business.Entities;
using HaloHistory.Business.Repositories.Metadata;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HaloHistory.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class MetadataController : Controller
    {
        private IMetadataRepository MetadataRepository { get; }
        private ICartographerContext Context { get; }
        public MetadataController(IMetadataRepository metadataRepository, ICartographerContext context)
        {
            MetadataRepository = metadataRepository;
            Context = context;
        }

        // GET api/values/5
        [HttpGet("initialize")]
        public async Task Get(int id)
        {
            await MetadataRepository.Initialize();
            await Context.CommitChanges();
        }
        
    }
}
