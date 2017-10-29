using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    [Area("Admin")]
    public class PortalController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Admin | Portal";
            ViewBag.Selected = "categories";
            ViewBag.SystemJsImportPath = "app/components/categories/main.js";
            return View();
        }
    }
}
