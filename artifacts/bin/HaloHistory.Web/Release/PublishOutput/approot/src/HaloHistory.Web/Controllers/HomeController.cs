using System;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace HaloHistory.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpContext _httpContext;
        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public IActionResult Index()
        {
            try
            {
                if (_httpContext.Request.Path.HasValue)
                {
                    string index = Url.Content("~/");
                    ViewBag.BaseHref = _httpContext.Request.Path.Value.Substring(0, index.Length);
                }
            }
            catch (Exception)
            {
                if (!_httpContext.Request.Path.ToString().EndsWith("/"))
                    Response.Redirect(_httpContext.Request.Path + "/");
            }
            return View();
        }
        
    }
}
