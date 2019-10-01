using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Pioneer.Blog.Entites;
using Pioneer.Blog.Models;
using Pioneer.Blog.Repositories;

namespace Pioneer.Blog.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetAllPaged(int count, int page);
        Category GetById(int id);
        Category Add(Category category);
        void Update(Category item);
        void Remove(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns>Collection of Categories</returns>
        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll()
                    .Select(_mapper.Map<CategoryEntity, Category>)
                    .OrderBy(x => x.Name)
                    .ToList();
        }

        /// <summary>
        /// Get paged collection of category
        /// </summary>
        /// <param name="count">Number of categories in page</param>
        /// <param name="page">Page of categories</param>
        /// <returns>Count of categories starting at page</returns>
        public IEnumerable<Category> GetAllPaged(int count, int page)
        {
            return _categoryRepository.GetAllPaged(count, page)
                    .Select(_mapper.Map<CategoryEntity, Category>);
        }

        /// <summary>
        /// Get Category by id
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Category Object</returns>
        public Category GetById(int id)
        {
            return _mapper.Map<CategoryEntity, Category>(_categoryRepository.GetById(id));
        }


        /// <summary>
        /// Create Category record
        /// If a Name is not provided, set Name and URL to a GUID
        /// </summary>
        /// <param name="category">Category</param>
        public Category Add(Category category)
        {
            if (category.Name == null)
            {
                category.Name = Guid.NewGuid().ToString();
                category.Url = category.Name;
            }

            var response = _categoryRepository.Add(_mapper.Map<Category, CategoryEntity>(category));
            category.CategoryId = response.CategoryId;
            return category;
        }

        /// <summary>
        /// Update Category record
        /// </summary>
        /// <param name="category">Updated Category</param>
        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }

        /// <summary>
        /// Delete Category record based on id
        /// </summary>
        /// <param name="id">Category Id</param>
        public void Remove(int id)
        {
            _categoryRepository.Remove(id);
        }
    }
}
