using BackendCourse.DTOs;
using BackendCourse.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpGet]
        public async Task<IEnumerable<PostDTO>> Get() => await _postService.Get();

    }
}
