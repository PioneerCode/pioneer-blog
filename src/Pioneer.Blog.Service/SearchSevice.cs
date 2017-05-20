using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Pioneer.Blog.DAL.Entites;
using Pioneer.Blog.Model;
using Pioneer.Blog.Repository;

namespace Pioneer.Blog.Service
{
    public interface ISearchService
    {
        SearchResults SearchPosts(string query, int count, int page = 1, bool includeUnpublished = false);
    }

    public class SearchSevice : ISearchService
    {
        private readonly IPostRepository _postRepository;

        public SearchSevice(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public SearchResults SearchPosts(string query, int count, int page = 1, bool includeUnpublished = false)
        {
            return new SearchResults
            {
                Posts = _postRepository.GetQueryPaged(query, count, page).Select(Mapper.Map<PostEntity, Post>),
                TotalMatchingPosts = _postRepository.GetQueryPagedCount(query)
            };
        }
    }
}
