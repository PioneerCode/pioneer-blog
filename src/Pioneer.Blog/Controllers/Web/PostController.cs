using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Model;
using Pioneer.Blog.Service;

namespace Pioneer.Blog.Controllers.Web
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index(string id)
        {
            var post = _postService.GetPreviousCurrentNextPost(id).ToList();
            ViewBag.Description = post[(int)PreviousCurrentNextPosition.Current].Description + " By Chad Ramos at Pioneer Code.";
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            return View(post);
        }
    }
}