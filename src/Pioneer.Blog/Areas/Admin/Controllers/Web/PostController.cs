using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    [Area("admin")]

    public class PostController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Admin Posts";
            return View();
        }
    }
}
