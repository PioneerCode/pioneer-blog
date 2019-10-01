using System.Linq;
using Pioneer.Blog.Entites;

namespace Pioneer.Blog.Repositories
{
    public interface IPostTagRepository
    {
        PostTagEntity Add(PostTagEntity map);
        void RemoveByCompound(PostTagEntity map);
    }

    public class PostTagRepository : IPostTagRepository
    {
        private readonly BlogDbContext _blogContext;

        public PostTagRepository(BlogDbContext blogContext)
        {
            _blogContext = blogContext;
        }

        /// <summary>
        /// Add a new PostTagEntity record
        /// </summary>
        /// <param name="postTag">Compound Key</param>
        /// <returns>Qualified PostTagEntity</returns>
        public PostTagEntity Add(PostTagEntity postTag)
        {
            _blogContext
                .PostTags
                .Add(postTag);

            _blogContext.SaveChanges();

            return postTag;
        }

        /// <summary>
        /// Delete a PostTagEntity by compound key
        /// </summary>
        /// <param name="postTag">Compound Key</param>
        public void RemoveByCompound(PostTagEntity postTag)
        {
            var entity = _blogContext
               .PostTags
               .FirstOrDefault(x => x.PostId == postTag.PostId && x.TagId == postTag.TagId);

            if (entity == null) return;

            _blogContext.PostTags.Remove(entity);
            _blogContext.SaveChanges();
        }
    }
}
