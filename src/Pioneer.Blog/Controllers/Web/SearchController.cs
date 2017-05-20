using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Service;
using Pioneer.Pagination;

namespace Pioneer.Blog.Controllers.Web
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IPaginatedMetaService _paginatedMetaService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IPostService _postService;

        public SearchController(ISearchService searchService, 
            IPaginatedMetaService paginatedMetaService, 
            ICategoryService categoryService, 
            ITagService tagService, 
            IPostService postService)
        {
            _searchService = searchService;
            _paginatedMetaService = paginatedMetaService;
            _categoryService = categoryService;
            _tagService = tagService;
            _postService = postService;
        }

        // GET: Search
        public ActionResult Index(string query, int page = 1)
        {
            var post = _searchService.SearchPosts(query, 10, page).ToList();
            ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(_postService.GetTotalNumberOfPosts(), page, 4);

            ViewBag.Description = "Pioneer Code blog archives page " + page + ". " +
                                  "Chad Ramos talks about .NET, C#, The Web, Open Source, Programming and more.";

            ViewBag.Header = "Search";
            ViewBag.Title = "Search";
            ViewBag.Pager = "search";
            ViewBag.Selected = "search";

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, false, 4).ToList();

            return View("../ArchivePost/Index", post);
        }
    }
}
