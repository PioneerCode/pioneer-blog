using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Model;
using Pioneer.Blog.Service;

namespace Pioneer.Blog.Controllers.Api
{
    [Authorize]
    [Route("api/posts")]
    public class PostApiController : Controller
    {
        private readonly IPostService _postService;

        public PostApiController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IEnumerable<Post> GetAll(int? countPerPage,
            int? currentPageIndex,
            bool includeExcerpt = true,
            bool includeArticle = true,
            bool includeUnpublished = false)
        {
            if (countPerPage == null || currentPageIndex == null)
            {
                return _postService.GetAll(includeExcerpt, includeArticle, includeUnpublished);
            }

            return _postService.GetAllPaged((int)countPerPage, (int)currentPageIndex, includeUnpublished);
        }

        [HttpGet("{url}", Name = "GetPost")]
        public IActionResult GetById(string url, bool includeExcerpt = false)
        {
            var item = _postService.GetByUrl(url, includeExcerpt);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [Route("count/total")]
        public IActionResult GetTotalNumberOfPosts()
        {
            return new ObjectResult(_postService.GetTotalNumberOfPosts());
        }

        [HttpPost]
        [Authorize(Policy = "isSuperUser")]
        public IActionResult Create([FromBody]Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            _postService.Add(post);
            return CreatedAtRoute("GetPost", new { url = post.Url }, post);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "isSuperUser")]
        public IActionResult Update(int id, [FromBody] Post item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = _postService.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _postService.Update(item);
            return new NoContentResult();
        }

        [HttpPut("import/excerpt/{id}")]
        [Authorize(Policy = "isSuperUser")]
        public IActionResult ImportExcerpt(int id)
        {
            var todo = _postService.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            _postService.Import(id, true);

            return new NoContentResult();
        }

        [HttpPut("import/article/{id}")]
        [Authorize(Policy = "isSuperUser")]
        public IActionResult ImportArticle(int id)
        {
            var todo = _postService.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }
            _postService.Import(id);
            return new NoContentResult();
        }

        [HttpGet("dev/markup/{id}")]
        [Authorize(Policy = "isSuperUser")]
        public IActionResult PostDevFile(int id)
        {
            var markup = _postService.GetDevFile(id);
            if (markup == null)
            {
                return NotFound();
            }
            return Ok(markup);
        }

        //[HttpDelete("{url}")]
        //[Authorize(Policy = "isSuperUser")]
        //public IActionResult Delete(string url)
        //{
        //    var todo = _postService.GetByUrl(url);
        //    if (todo == null)
        //    {
        //        return NotFound();
        //    }

        //    _postService.Remove(url);
        //    return new NoContentResult();
        //}
    }
}
