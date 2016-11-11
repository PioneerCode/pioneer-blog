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
            ViewBag.Description = "About Pioneer Code and Chad Ramos";
            return View();
        }
    }
}