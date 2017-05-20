using Pioneer.Blog.Service;

namespace Pioneer.Blog.Controllers.Web
{
    public class SearchController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
    }
}
