using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Areas.Admin.Controllers.Web
{
    [Area("admin")]

    public class TagController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Admin Tags";
            return View();
        }
    }
}
