using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Server.Migrations;
using RedditClone.Shared.Enums;

namespace RedditClone.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IVoteService _voteService;

        public CommentController(ICommentService commentService, IVoteService voteService)
        {
            _commentService = commentService;
            _voteService = voteService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<Comment>>> GetCommentsAsync()
        {
            var response = await _commentService.GetCommentsAsync();
            return Ok(response);
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

        [HttpPost("vote/get")]
        public async Task<ActionResult<ServiceResponse<SubmissionVote>>> GetCommentVoteAsync(VoteDTO vote)
        {
            var response = await _voteService.GetSubmissionVoteAsync(vote, VoteKind.Comment);
            return Ok(response);
        }

        [HttpPost("vote/up"), Authorize]
        public async Task<ActionResult<ServiceResponse<SubmissionVote>>> UpVoteCommentAsync(VoteDTO vote)
        {
            var response = vote.Existing ? await _voteService.EditSubmissionVoteAsync(vote, VoteType.Up, VoteKind.Comment)
                : await _voteService.AddSubmissionVoteAsync(vote, VoteType.Up, VoteKind.Comment);
            return Ok(response);
        }

        [HttpPost("vote/down"), Authorize]
        public async Task<ActionResult<ServiceResponse<SubmissionVote>>> DownVoteCommentAsync(VoteDTO vote)
        {
            var response = vote.Existing ? await _voteService.EditSubmissionVoteAsync(vote, VoteType.Down, VoteKind.Comment)
                : await _voteService.AddSubmissionVoteAsync(vote, VoteType.Down, VoteKind.Comment);
            return Ok(response);
        }

        [HttpPost("vote/remove"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> UnVoteCommentAsync(VoteDTO vote)
        {
            var response = await _voteService.RemoveSubmissionVoteAsync(vote, VoteKind.Comment);
            return Ok(response);
        }
    }
}
