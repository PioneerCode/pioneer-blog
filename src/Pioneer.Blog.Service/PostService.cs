using System.Collections.Generic;
using AutoMapper;
using Pioneer.Blog.DAL.Entites;
using Pioneer.Blog.Model;
using System.Linq;
using Pioneer.Blog.Repository;
using Microsoft.Extensions.Options;

namespace Pioneer.Blog.Service
{
    public interface IPostService
    {
        int GetTotalNumberOfPosts();
        int GetTotalNumberOfPostsByCategory(string category);
        int GetTotalNumberOfPostByTag(string tag);
        Post GetById(string id);
        IEnumerable<Post> GetAll(int? top = null);
        IEnumerable<Post> GetAllPaged(int count, int page = 1);
        IEnumerable<Post> GetAllByTag(string tag, int count, int page = 1);
        IEnumerable<Post> GetAllByCategory(string category, int count, int page = 1);
        IEnumerable<Post> GetPopularPosts();
        IEnumerable<Post> GetPreviousCurrentNextPost(string id);
        Post Add(Post post);
        void Update(Post item);
        void Remove(int id);
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IOptions<AppConfiguration> _appConfiguration;

        public PostService(IPostRepository postRepository,
            IOptions<AppConfiguration> appConfiguration)
        {
            _postRepository = postRepository;
            _appConfiguration = appConfiguration;
        }

        /// <summary>
        /// Get total number of posts
        /// </summary>
        public int GetTotalNumberOfPosts()
        {
            return _postRepository.GetTotalNumberOfPosts();
        }

        /// <summary>
        /// Get total number of posts by category
        /// </summary>
        /// <param name="category">category url</param>
        public int GetTotalNumberOfPostsByCategory(string category)
        {
            return _postRepository.GetTotalNumberOfPostsByCategory(category);
        }

        /// <summary>
        /// Get total number of posts by tag
        /// </summary>
        /// <param name="tag">tag url</param>
        public int GetTotalNumberOfPostByTag(string tag)
        {
            return _postRepository.GetTotalNumberOfPostByTag(tag);
        }

        /// <summary>
        /// Get Post by id
        /// </summary>
        /// <param name="id">Id of post</param>
        /// <returns>Post</returns>
        public Post GetById(string id)
        {
            return Mapper.Map<PostEntity, Post>(_postRepository.GetById(id));
        }

        /// <summary>
        /// Get all posts or get top posts
        /// </summary>
        /// <param name="top">Take top.  If null returns all posts</param>
        /// <returns>Collection of Post</returns>
        public IEnumerable<Post> GetAll(int? top = null)
        {
            var posts = top != null
                ? _postRepository.GetTop((int)top).ToList()
                : _postRepository.GetAll().ToList();

            return posts.Select(Mapper.Map<PostEntity, Post>);
        }

        /// <summary>
        /// Get paged collection of posts
        /// </summary>
        /// <param name="count">Number of posts in page</param>
        /// <param name="page">Page of posts</param>
        /// <returns>Count of posts starting at page</returns>
        public IEnumerable<Post> GetAllPaged(int count, int page = 1)
        {
            return _postRepository.GetAllPaged(count, page).Select(Mapper.Map<PostEntity, Post>);
        }

        /// <summary>
        /// Get paged collection of posts, filtered by tag
        /// </summary>
        /// <param name="tag">Filter by tag</param>
        /// <param name="count">Number of posts in page</param>
        /// <param name="page">Page of posts</param>
        /// <returns>Count of posts starting at page</returns>
        public IEnumerable<Post> GetAllByTag(string tag, int count, int page = 1)
        {
            return Mapper.Map<IList<PostEntity>, IList<Post>>(_postRepository.GetAllByTagPaged(tag, count, page).ToList());
        }

        /// <summary>
        /// Get paged collection of posts, filtered by category
        /// </summary>
        /// <param name="category">Filter by category</param>
        /// <param name="count">Number of posts in page</param>
        /// <param name="page">Page of posts</param>
        /// <returns>Count of posts starting at page</returns>
        public IEnumerable<Post> GetAllByCategory(string category, int count, int page = 1)
        {
            return Mapper.Map<IList<PostEntity>, IList<Post>>(_postRepository.GetAllByCategoryPaged(category, count, page).ToList());
        }

        /// <summary>
        /// Get popular post based on ids in app settings
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Post> GetPopularPosts()
        {
            return Mapper.Map<IList<PostEntity>, IList<Post>>(_postRepository.GetPostsBasedOnIdCollection(_appConfiguration.Value.PopularPosts).ToList());
        }

        /// <summary>
        /// Get Previous, Current and Next post based on id
        /// </summary>
        /// <param name="id">Current post url</param>
        /// <returns>Collection with first index being previous, second index being current and third index being next</returns>
        // TODO: Profile and work to reduce tips to the database
        public IEnumerable<Post> GetPreviousCurrentNextPost(string id)
        {
            var currentPost = _postRepository.GetById(id);
            var posts = new List<PostEntity>
            {
                _postRepository.GetPreviousBasedOnId(currentPost.PostId),
                currentPost,
                _postRepository.GetNextBasedOnId(currentPost.PostId)
            };

            return Mapper.Map<IList<PostEntity>, IList<Post>>(posts);
        }

        /// <summary>
        /// Add Post record
        /// </summary>
        /// <param name="post">Post object</param>
        /// <returns>New Post record</returns>
        public Post Add(Post post)
        {
            var response = _postRepository.Add(Mapper.Map<Post, PostEntity>(post));
            post.PostId = response.PostId;
            return post;
        }

        /// <summary>
        /// Update Post record
        /// </summary>
        /// <param name="post">Post Object</param>
        public void Update(Post post)
        {
            _postRepository.Update(post);
        }

        /// <summary>
        /// Delete Post record
        /// </summary>
        /// <param name="url">Post url</param>
        public void Remove(int url)
        {
            _postRepository.Remove(url);
        }
    }
}
