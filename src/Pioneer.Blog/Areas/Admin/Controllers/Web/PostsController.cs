using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    [Area("admin")]
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Admin | Posts";
            ViewBag.SystemJsImportPath = "app/components/apps/posts/main.js";
            return View();
        }
    }
}
