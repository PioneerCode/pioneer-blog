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
                "Hi, my name is Chad Ramos. I am a Chicago-based software developer with a strong passion for .NET, C#, The Web, Open Source, Programming and more. Brought to you by Pioneer Code.";

            ViewBag.PopularPosts = _postService.GetPopularPosts().ToList();
            ViewBag.LatestPosts = _postService.GetAll(true, false, false, 8).ToList();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //TODO: Figure out how to combine logic with Index
        public ActionResult SignUp(SignUpViewModel model)
        {
            ViewBag.Description =
                "Sign up for How-to's, life hacks and insight into .NET, C#, The Web, Open Source, Programming and more from Chad Ramos. Brought to you by Pioneer Code.";

            ViewBag.PopularPosts = _postService.GetPopularPosts().ToList();
            ViewBag.LatestPosts = _postService.GetAll(true, false, false, 8).ToList();
            ViewBag.Anchor = "#home-sign-up";

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