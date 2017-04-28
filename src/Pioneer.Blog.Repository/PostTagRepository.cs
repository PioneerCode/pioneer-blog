using System.Linq;
using Pioneer.Blog.DAL;
using Pioneer.Blog.DAL.Entites;

namespace Pioneer.Blog.Repository
{
    public interface IPostTagRepository
    {
        PostTagEntity Add(PostTagEntity map);
        void RemoveByCompound(PostTagEntity map);
    }

    public class PostTagRepository : IPostTagRepository
    {
        private readonly BlogContext _blogContext;

        public PostTagRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public PostTagEntity Add(PostTagEntity postTag)
        {
            _blogContext
                .PostTags
                .Add(postTag);

            _blogContext.SaveChanges();

            return postTag;
        }

        public void RemoveByCompound(PostTagEntity postTag)
        {
            var entity = _blogContext
               .PostTags
               .FirstOrDefault(x => x.PostId == postTag.PostId && x.TagId == postTag.TagId);

            _blogContext.PostTags.Remove(entity);
            _blogContext.SaveChanges();
        }
    }
}
