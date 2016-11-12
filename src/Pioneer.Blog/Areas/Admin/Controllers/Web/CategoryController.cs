using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    [Area("admin")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Admin Categories";
            return View();
        }
    }
}
