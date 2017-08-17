#if (DEBUG)
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    [Area("Admin")]
    [Authorize]
    public class TagsController : Controller
    {
        public IActionResult Index()
        { 
            ViewBag.Title = "Admin | Tags";
            ViewBag.Selected = "tags";
            ViewBag.SystemJsImportPath = "app/components/tags/main.js";
            return View();
        }
    }
}
#endif