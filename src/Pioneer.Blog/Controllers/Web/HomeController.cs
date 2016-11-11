using System.Text;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Service;

namespace Pioneer.Blog.Controllers.Web
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ISiteMapService _siteMapService;

        public HomeController(IPostService postService, 
            ISiteMapService siteMapService)
        {
            _postService = postService;
            _siteMapService = siteMapService;
        }

        public IActionResult Index()
        {
            ViewBag.Description =
                "PioneerCode.com | How-to's, life hacks and observations for the modern day software developer.";
            return View(_postService.GetAll(3));
        }

        public IActionResult SiteMap()
        {
            return Content(_siteMapService.GetSiteMap(), "text/xml", Encoding.UTF8);
        }
    }
}