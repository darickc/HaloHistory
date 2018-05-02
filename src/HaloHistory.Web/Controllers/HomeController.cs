using Microsoft.AspNet.Http.Extensions;
using Microsoft.AspNet.Mvc;

namespace HaloHistory.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (Request.Path.HasValue && !Request.Path.Value.EndsWith("/"))
            {
                return Redirect(Request.Path.Value + "/");
            }
            if (!Request.GetDisplayUrl().EndsWith("/"))
            {
                return Redirect(Request.GetDisplayUrl() + "/");
            }

            ViewBag.BaseHref = Request.PathBase + "/";
            return View();
        }

    }
}
