using System.Collections.Generic;
using AutoMapper;
using Pioneer.Blog.DAL.Entites;
using Pioneer.Blog.Model;
using System.Linq;
using Pioneer.Blog.Repository;

namespace Pioneer.Blog.Service
{
    public interface ICategoryService
    {
        List<Category> GetAll();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository
                    .GetAll()
                    .Select(Mapper.Map<CategoryEntity, Category>).ToList();
        }
    }
}
