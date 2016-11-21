using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Model;
using Pioneer.Blog.Service;

namespace Pioneer.Blog.Areas.Admin.Controllers.Api
{
    [Route("api/categories")]
    public class CategoryApiController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<Category> GetAll(int? count, int? page)
        {
            if (count == null || page == null)
            {
                return _categoryService.GetAll();
            }

            return _categoryService.GetAllPaged((int)count, (int)page);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult GetById(int id)
        {
            var item = _categoryService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Category tag)
        {
            if (tag == null)
            {
                return BadRequest();
            }

            _categoryService.Add(tag);
            return CreatedAtRoute("GetCategory", new { id = tag.CategoryId }, tag);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category item)
        {
            if (item == null || item.CategoryId != id)
            {
                return BadRequest();
            }

            var todo = _categoryService.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _categoryService.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _categoryService.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _categoryService.Remove(id);
            return new NoContentResult();
        }
    }
}
