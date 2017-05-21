using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Model;
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

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Description = "Pioneer Code search page. "  +
                                  "Chad Ramos talks about .NET, C#, The Web, Open Source, Programming and more.";

            ViewBag.Header = "Search";
            ViewBag.Title = "Search";
            ViewBag.Pager = "search";
            ViewBag.Selected = "search";

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, false, 4).ToList();

            return View("../Search/Index", new List<Post>());
        }

        [HttpPost]
        public ActionResult Query(SearchRequest request)
        {
            var searchResults = _searchService.SearchPosts(request.Query, 5, request.Page);
            ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(searchResults.TotalMatchingPosts, request.Page, 5);

            ViewBag.Description = "Pioneer Code search results for \"" + request.Query + "\", page " + request.Page + ". " +
                                  "Chad Ramos talks about .NET, C#, The Web, Open Source, Programming and more.";

            ViewBag.Header = "Search";
            ViewBag.Title = "Search";
            ViewBag.Selected = "search";
            ViewBag.Query = request.Query;

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, false, 4).ToList();

            return View("../Search/Index", searchResults.Posts);
        }

        [HttpGet("search/{query}/{page:int?}")]
        public ActionResult GetQuery(string query, int page = 1)
        {
            var searchResults = _searchService.SearchPosts(query, 5, page);
            ViewBag.PaginatedMeta = _paginatedMetaService.GetMetaData(searchResults.TotalMatchingPosts, page, 5);

            ViewBag.Description = "Pioneer Code search results for \"" + query + "\", page " + page + ". " +
                                  "Chad Ramos talks about .NET, C#, The Web, Open Source, Programming and more.";

            ViewBag.Header = "Search";
            ViewBag.Title = "Search";
            ViewBag.Selected = "search";
            ViewBag.Query = query;

            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Tags = _tagService.GetAll();
            ViewBag.PopularPosts = _postService.GetPopularPosts();
            ViewBag.NewPosts = _postService.GetAll(true, false, false, 4).ToList();

            return View("../Search/Index", searchResults.Posts);
        }
    }
}
