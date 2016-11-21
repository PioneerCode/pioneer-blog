using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    [Area("Admin")]
    [Authorize]
    public class CategoriesController : Controller
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
