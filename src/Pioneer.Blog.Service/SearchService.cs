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
        SearchResults SearchPosts(string query, int count, int page = 1);
    }

    public class SearchService : ISearchService
    {
        private readonly IPostRepository _postRepository;

        public SearchService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public SearchResults SearchPosts(string query, int count, int page = 1)
        {
            return new SearchResults
            {
                Posts = _postRepository.GetQueryPaged(query, count, page).Select(Mapper.Map<PostEntity, Post>).ToList(),
                TotalMatchingPosts = _postRepository.GetQueryPagedCount(query)
            };
        }
    }
}
