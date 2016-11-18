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

            ViewBag.Description = "Pioneer Code Blog Archives";
            ViewBag.Header = "Blog";
            ViewBag.Title = "Blog";
            ViewBag.Pager = "blog";

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, 4).ToList();

            return View("../ArchivePost/Index", post);
        }

        // GET: Tag
        public ActionResult Tag(string id, int page = 1)
        {
            var posts = _postService.GetAllByTag(id, 4, page).ToList();
            ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(_postService.GetTotalNumberOfPostByTag(id), page, 4);

            ViewBag.Title = _tagService.GetTagNameFromTagUrlInTagCollection(id, posts[0].Tags.ToList());
            ViewBag.Description = "Pioneer Code Blog Tag Archives - " + ViewBag.Tag;
            ViewBag.Header = "Tag : " + posts[0].Tags.ElementAt(0).Name;
            ViewBag.Pager = "tag";

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, 4).ToList();

            return View("../ArchivePost/Index", posts);
        }

        // GET: Category
        public ActionResult Category(string id, int page = 1)
        {
            var posts = _postService.GetAllByCategory(id, 4, page).ToList();
            ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(_postService.GetTotalNumberOfPostsByCategory(id), page, 4);

            ViewBag.Description = "Pioneer Code Blog Category Archives - " + ViewBag.Tag;
            ViewBag.Header = "Category : " + posts[0].Category.Name;
            ViewBag.Title = posts[0].Category.Name;
            ViewBag.Pager = "category";

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, 4).ToList();

            return View("../ArchivePost/Index", posts);
        }
    }
}