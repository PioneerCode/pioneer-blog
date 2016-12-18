using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Model.Views;

namespace Pioneer.Blog.Controllers.Web
{
    public class ArticleController : Controller
    {
        // GET: Blog
        public ActionResult VisualStudioShortcuts()
        {
            ViewBag.Title = "Visual Studio Shortcuts";
            ViewBag.Description = "Visual Studio Shortcuts";
            ViewBag.Selected = "visual-studio-shortcuts";
            return View("~/Views/Article/VisualStudioShortcuts.cshtml", new ArticleViewModel());
        }
    }
}
