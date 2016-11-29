using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.DAL;
using Pioneer.Blog.Model.Views;
using Pioneer.Blog.Service;

namespace Pioneer.Blog.Controllers.Web
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ISiteMapService _siteMapService;
        private readonly ICommunicationService _communicationService;

        public HomeController(IPostService postService,
            ISiteMapService siteMapService, ICommunicationService communicationService)
        {
            _postService = postService;
            _siteMapService = siteMapService;
            _communicationService = communicationService;
        }

        public IActionResult Index()
        {
            ViewBag.Description =
                "PioneerCode.com | How-to's, life hacks and observations for the modern day software developer.";

            ViewBag.PopularPosts = _postService.GetPopularPosts().ToList();
            ViewBag.LatestPosts = _postService.GetAll(true, false, false, 4).ToList();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //TODO: Figure out how to combine logic with Index
        public ActionResult SignUp(SignUpViewModel model)
        {
            ViewBag.Description =
                "PioneerCode.com | How-to's, life hacks and observations for the modern day software developer.";

            ViewBag.PopularPosts = _postService.GetPopularPosts().ToList();
            ViewBag.LatestPosts = _postService.GetAll(true, false, false, 4).ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.IsValid = false;
                return View("Index", model);
            }

            var response = _communicationService.SignUpToMailingList(model);

            if (response.Status != OperationStatus.Created)
            {
                ViewBag.IsValid = false;
                ViewBag.IsValidMessage = response.Message;
                return View("Index", model);
            }

            ViewBag.IsValid = true;

            return View("Index", model);
        }

        public IActionResult SiteMap()
        {
            return Content(_siteMapService.GetSiteMap(), "text/xml", Encoding.UTF8);
        }
    }
}