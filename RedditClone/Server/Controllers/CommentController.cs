using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Server.Services.CommentService;

namespace RedditClone.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("get/{hash}")]
        public async Task<ActionResult<ServiceResponse<Comment>>> GetCommentByHashAsync(string hash)
        {
            var response = await _commentService.GetCommentByHashAsync(hash);
            return Ok(response);
        }

        [HttpGet("count/{hash}")]
        public async Task<ActionResult<ServiceResponse<int>>> CountCommentsByPostHashAsync(string hash)
        {
            var response = await _commentService.CountCommentsByPostHashAsync(hash);
            return Ok(response);
        }

        [HttpGet("get/parent/{hash}")]
        public async Task<ActionResult<ServiceResponse<List<Comment>>>> GetCommentsByParentHashAsync(string hash)
        {
            var response = await _commentService.GetCommentsByParentHashAsync(hash);
            return Ok(response);
        }

        [HttpGet("get/post/{hash}")]
        public async Task<ActionResult<ServiceResponse<List<Comment>>>> GetCommentsByPostHashAsync(string hash)
        {
            var response = await _commentService.GetCommentsByPostHashAsync(hash);
            return Ok(response);
        }

        [HttpGet("get/author/{guid}")]
        public async Task<ActionResult<ServiceResponse<List<Comment>>>> GetCommentsByAuthorAsync(Guid guid)
        {
            var response = await _commentService.GetCommentsByAuthorAsync(guid);
            return Ok(response);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ServiceResponse<Comment>>> AddCommentAsync(CommentNew comment)
        {
            var response = await _commentService.AddCommentAsync(comment);
            return Ok(response);
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<ServiceResponse<Comment>>> EditCommentAsync(CommentEdit edit)
        {
            var response = await _commentService.EditCommentAsync(edit);
            return Ok(response);
        }
    }
}
