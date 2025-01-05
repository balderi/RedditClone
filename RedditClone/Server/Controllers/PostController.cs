using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Server.Services.VoteService;
using RedditClone.Shared.Enums;

namespace RedditClone.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IVoteService _voteService;

        public PostController(IPostService postService, IVoteService voteService)
        {
            _postService = postService;
            _voteService = voteService;
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

        [HttpPut("edit"), Authorize]
        public async Task<ActionResult<ServiceResponse<Post>>> EditPostAsync(PostEdit edit)
        {
            var response = await _postService.EditPostAsync(edit);
            return Ok(response);
        }

        [HttpPost("vote/get")]
        public async Task<ActionResult<ServiceResponse<SubmissionVote>>> GetPostVoteAsync(VoteDTO vote)
        {
            var response = await _voteService.GetSubmissionVoteAsync(vote, VoteKind.Post);
            return Ok(response);
        }

        [HttpPost("vote/up"), Authorize]
        public async Task<ActionResult<ServiceResponse<SubmissionVote>>> UpVotePostAsync(VoteDTO vote)
        {
            var response = vote.Existing ? await _voteService.EditSubmissionVoteAsync(vote, VoteType.Up, VoteKind.Post)
                : await _voteService.AddSubmissionVoteAsync(vote, VoteType.Up, VoteKind.Post);
            return Ok(response);
        }

        [HttpPost("vote/down"), Authorize]
        public async Task<ActionResult<ServiceResponse<SubmissionVote>>> DownVotePostAsync(VoteDTO vote)
        {
            var response = vote.Existing ? await _voteService.EditSubmissionVoteAsync(vote, VoteType.Down, VoteKind.Post)
                : await _voteService.AddSubmissionVoteAsync(vote, VoteType.Down, VoteKind.Post);
            return Ok(response);
        }

        [HttpPost("vote/remove"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> UnVotePostAsync(VoteDTO vote)
        {
            var response = await _voteService.RemoveSubmissionVoteAsync(vote, VoteKind.Post);
            return Ok(response);
        }
    }
}
