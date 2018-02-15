using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    [Area("Admin")]
    public class PortalController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Admin | Portal";
            return File("~/admin/index.html", "text/html");
        }
    }
}
