using System.Collections.Generic;
using Pioneer.Blog.DAL;
using Pioneer.Blog.DAL.Entites;
using System.Linq;

namespace Pioneer.Blog.Repository
{
    public interface ITagRepository
    {
        List<TagEntity> GetAll();
    }

    public class TagRepository : ITagRepository
    {
        private readonly BlogContext _blogContext;

        public TagRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<TagEntity> GetAll()
        {
          return _blogContext
                    .Tags
                    .ToList();
        }
    }
}
