using System.Collections.Generic;
using System.Linq;
using Pioneer.Blog.Entity;
using Pioneer.Blog.Model;

namespace Pioneer.Blog.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryEntity> GetAll();
        IEnumerable<CategoryEntity> GetAllPaged(int count, int page);
        CategoryEntity GetById(int id);
        CategoryEntity Add(CategoryEntity map);
        void Update(Category category);
        void Remove(int id);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogDbContext _blogContext;

        public CategoryRepository(BlogDbContext blogContext)
        {
            _blogContext = blogContext;
        }

        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns>Category Collection</returns>
        public IEnumerable<CategoryEntity> GetAll()
        {
            return _blogContext
                      .Categories
                      .ToList();
        }

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category Entity</returns>
        public CategoryEntity GetById(int id)
        {
            return _blogContext
                    .Categories
                    .FirstOrDefault(x => x.CategoryId == id);
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="categoryEntity">Category Entity to save</param>
        /// <returns>Category Entity with Id</returns>
        public CategoryEntity Add(CategoryEntity categoryEntity)
        {
            _blogContext
                .Categories
                .Add(categoryEntity);
            _blogContext.SaveChanges();

            return categoryEntity;
        }

        /// <summary>
        /// Update Entity based on Model
        /// </summary>
        /// <param name="category">Category Object</param>
        public void Update(Category category)
        {
            var entity = _blogContext
                .Categories
                .FirstOrDefault(x => x.CategoryId == category.CategoryId);

            if (entity == null) return;

            entity.Url = category.Url;
            entity.Name = category.Name;

            _blogContext.SaveChanges();
        }

        /// <summary>
        /// Remove Category record based on Id
        /// </summary>
        /// <param name="id">Category Id</param>
        public void Remove(int id)
        {
            var entity = _blogContext
                .Categories
                .FirstOrDefault(x => x.CategoryId == id);

            if (entity == null) return;

            _blogContext.Categories.Remove(entity);
            _blogContext.SaveChanges();
        }

        /// <summary>
        /// Get a collection of categories by skipping x and taking y
        /// </summary>
        /// <param name="count">The total number of categories you want to Take</param>
        /// <param name="page">The denomination of categories you want to skip. (page - 1) * count </param>
        /// <returns>Collections of Categories</returns>
        public IEnumerable<CategoryEntity> GetAllPaged(int count, int page)
        {
            return _blogContext
                    .Categories
                    .Skip((page - 1) * count)
                    .Take(count)
                    .ToList();
        }
    }
}
