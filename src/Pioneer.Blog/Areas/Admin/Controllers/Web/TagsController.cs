using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    [Area("admin")]
    public class TagsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Admin | Tags";
            ViewBag.SystemJsImportPath = "app/components/apps/tags/main.js";
            return View();
        }
    }
}
