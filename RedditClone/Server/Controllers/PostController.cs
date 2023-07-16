using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedditClone.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("get/{hashID}")]
        public async Task<ActionResult<ServiceResponse<Post>>> GetPostByHashIDAsync(string hashID)
        {
            var response = await _postService.GetPostByHashIdAsync(hashID);
            return Ok(response);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<ServiceResponse<List<Post>>>> GetPostsAsync()
        {
            var response = await _postService.GetPostsAsync();
            return Ok(response);
        }

        [HttpGet("get/author/{guid}")]
        public async Task<ActionResult<ServiceResponse<List<Post>>>> GetPostsByAuthorAsync(Guid guid)
        {
            var response = await _postService.GetPostsByAuthorAsync(guid);
            return Ok(response);
        }

        [HttpGet("get/board/{name}")]
        public async Task<ActionResult<ServiceResponse<List<Post>>>> GetPostsByBoardNameAsync(string name)
        {
            var response = await _postService.GetPostsByBoardNameAsync(name);
            return Ok(response);
        }

        [HttpGet("get/board/guid/{guid}")]
        public async Task<ActionResult<ServiceResponse<List<Post>>>> GetPostsByBoardGuidAsync(Guid guid)
        {
            var response = await _postService.GetPostsByBoardGuidAsync(guid);
            return Ok(response);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ServiceResponse<Post>>> AddPostAsync(PostNew post)
        {
            var response = await _postService.AddPostAsync(post);
            return Ok(response);
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<ServiceResponse<Post>>> EditPostAsync(PostEdit edit)
        {
            var response = await _postService.EditPostAsync(edit);
            return Ok(response);
        }
    }
}
