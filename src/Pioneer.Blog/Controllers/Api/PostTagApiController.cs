using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Blog.Model;
using Pioneer.Blog.Service;

namespace Pioneer.Blog.Controllers.Api
{
    [Authorize]
    [Route("api/post-tags")]
    public class PostTagApiController : Controller
    {
        private readonly IPostTagService _postTagService;

        public PostTagApiController(IPostTagService postTagService)
        {
            _postTagService = postTagService;
        }

        [HttpPost]
        public IActionResult Create([FromBody]PostTag postTag)
        {
            var newPostTage = _postTagService.Add(postTag);
            return CreatedAtRoute("GetPost", new { url = newPostTage.PostTagId }, newPostTage);
        }

        [HttpPost]
        [Route("remove/compound")]
        public IActionResult DeletePostTag([FromBody]PostTag postTag)
        {
            _postTagService.Delete(postTag);
            return new NoContentResult();
        }
    }
}
