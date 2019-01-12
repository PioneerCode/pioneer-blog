using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Service;
using System.Linq;
using Pioneer.Pagination;

namespace Pioneer.Blog.Controllers.Web
{
    public class BlogController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IPaginatedMetaService _paginatedMetaService;

        public BlogController(IPostService postService,
            ICategoryService categoryService,
            ITagService tagService,
            IPaginatedMetaService paginatedMetaService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
            _paginatedMetaService = paginatedMetaService;
        }

        // GET: Blog
        public ActionResult Index(int page = 1)
        {
            var post = _postService.GetAllPaged(4, page).ToList();
            ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(_postService.GetTotalNumberOfPosts(), page, 4);

            ViewBag.Description =
                $"Pioneer Code blog archives page {page}. Chad Ramos talks about .NET, C#, The Web, Open Source, Programming and more.";
            ViewBag.Header = "Blog";
            ViewBag.Title = "Blog";
            ViewBag.Pager = "blog";
            ViewBag.Selected = "blog";

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, false, 4).ToList();

            return View("~/Views/ArchivePost/Index.cshtml", post);
        }

        // GET: Tag
        public ActionResult Tag(string id, int page = 1)
        {
            var posts = _postService.GetAllByTag(id, 4, page).ToList();
            var tag = posts[0].Tags.Where(x => x.Url == id).ToList()[0].Name;
            ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(_postService.GetTotalNumberOfPostByTag(id), page, 4);

            ViewBag.Title = _tagService.GetTagNameFromTagUrlInTagCollection(id, posts[0].Tags.ToList());
            ViewBag.Description =
                $"Pioneer Code blog tag archives page {page}, for tag \"{tag}\". Chad Ramos talks about .NET, C#, The Web, Open Source, Programming and more.";
            ViewBag.Header = "Tag : " + tag;
            ViewBag.Pager = "tag/" + id;
            ViewBag.Selected = "blog";

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, false, 4).ToList();

            return View("~/Views/ArchivePost/Index.cshtml", posts);
        }

        // GET: Category
        public ActionResult Category(string id, int page = 1)
        {
            var posts = _postService.GetAllByCategory(id, 4, page).ToList();
            ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(_postService.GetTotalNumberOfPostsByCategory(id), page, 4);

            ViewBag.Description =
                $"Pioneer Code blog category archives page {page}, for category \"{posts[0].Category.Name}\". Chad Ramos talks about .NET, C#, the web, open source, programming and more.";
            ViewBag.Header = "Category : " + posts[0].Category.Name;
            ViewBag.Title = posts[0].Category.Name;
            ViewBag.Pager = "category/" + id;
            ViewBag.Selected = "blog";

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, false, 4).ToList();
            return View("~/Views/ArchivePost/Index.cshtml", posts);
        }
    }
}