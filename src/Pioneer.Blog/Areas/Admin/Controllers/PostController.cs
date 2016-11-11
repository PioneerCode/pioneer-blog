using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers
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
