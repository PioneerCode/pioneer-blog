using System.Linq;
using AutoMapper;
using Pioneer.Blog.Entity;
using Pioneer.Blog.Model;
using Pioneer.Blog.Repository;

namespace Pioneer.Blog.Service
{
    public interface ISearchService
    {
        SearchResults SearchPosts(string query, int count, int page = 1);
    }

    public class SearchService : ISearchService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public SearchService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public SearchResults SearchPosts(string query, int count, int page = 1)
        {
            var searchResults = new SearchResults();

            if (string.IsNullOrEmpty(query)) return searchResults;

            searchResults.Posts = _postRepository.GetQueryPaged(query, count, page).Select(_mapper.Map<PostEntity, Post>).ToList();
            searchResults.TotalMatchingPosts = _postRepository.GetQueryPagedCount(query);

            return searchResults;
        }
    }
}
