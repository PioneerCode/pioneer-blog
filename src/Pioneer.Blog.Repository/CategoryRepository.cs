using System.Collections.Generic;
using Pioneer.Blog.DAL;
using Pioneer.Blog.DAL.Entites;
using System.Linq;

namespace Pioneer.Blog.Repository
{
    public interface ICategoryRepository
    {
        List<CategoryEntity> GetAll();
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogContext _blogContext;

        public CategoryRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<CategoryEntity> GetAll()
        {
          return _blogContext
                    .Categories
                    .ToList();
        }
    }
}
