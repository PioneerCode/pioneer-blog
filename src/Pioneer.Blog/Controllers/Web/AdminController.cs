using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Controllers.Web
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return File("~/admin/index.html", "text/html");
        }
    }
}
