using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Model.Views;

namespace Pioneer.Blog.Controllers.Web
{
    public class ArticleController : Controller
    {
        // GET: Blog
        public ActionResult VisualStudioShortcuts()
        {
            ViewBag.Description = "Visual Studio Shortcuts";
            return View("~/Views/Article/VisualStudioShortcuts.cshtml", new ArticleViewModel());
        }
    }
}
