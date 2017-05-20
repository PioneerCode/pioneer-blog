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
        IEnumerable<Post> SearchPosts(string query, int count, int page = 1, bool includeUnpublished = false);
    }

    public class SearchSevice : ISearchService
    {
        private readonly IPostRepository _postRepository;

        public SearchSevice(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IEnumerable<Post> SearchPosts(string query, int count, int page = 1, bool includeUnpublished = false)
        {
            return _postRepository.GetAllPaged(count, page, includeUnpublished).Select(Mapper.Map<PostEntity, Post>);
        }
    }
}
