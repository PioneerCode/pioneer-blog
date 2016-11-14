using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Model;
using Pioneer.Blog.Service;

namespace Pioneer.Blog.Areas.Admin.Controllers.Api
{
    [Route("api/posts")]
    public class PostApiController : Controller
    {
        private readonly IPostService _postService;

        public PostApiController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IEnumerable<Post> GetAll(int? count, int? page)
        {
            if (count == null || page == null)
            {
                return _postService.GetAll();
            }

            return _postService.GetAllPaged((int)count, (int)page);
        }

        [HttpGet("{id}", Name = "GetPost")]
        public IActionResult GetById(string url)
        {
            var item = _postService.GetById(url);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            _postService.Add(post);
            return CreatedAtRoute("GetPost", new { id = post.PostId }, post);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string url, [FromBody] Post item)
        {
            if (item == null || item.Url != url)
            {
                return BadRequest();
            }

            var todo = _postService.GetById(url);
            if (todo == null)
            {
                return NotFound();
            }

            _postService.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string url)
        {
            var todo = _postService.GetById(url);
            if (todo == null)
            {
                return NotFound();
            }

            _postService.Remove(url);
            return new NoContentResult();
        }
    }
}
