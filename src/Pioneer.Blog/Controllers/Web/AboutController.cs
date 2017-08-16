using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Controllers.Web
{
    public class AboutController : Controller
    {
        /// <summary>
        /// GET: About
        /// </summary>
        public IActionResult Index()
        {
            ViewBag.Description = "Learn more about Chad Ramos and Pioneer Code, .NET, C#, The Web, Open Source, Programming and more.";
            ViewBag.Selected = "about";
            return View();
        }
    }
}