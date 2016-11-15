using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    [Area("admin")]
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Admin | Categories";
            ViewBag.SystemJsImportPath = "app/components/apps/categories/main.js";
            return View();
        }
    }
}
