using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Admin | Categories";
            ViewBag.Selected = "categories";
            ViewBag.SystemJsImportPath = "app/components/categories/main.js";
            return View();
        }
    }
}
