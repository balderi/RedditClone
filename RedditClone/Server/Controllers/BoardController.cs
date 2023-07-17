using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedditClone.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet("get/{boardName}")]
        public async Task<ActionResult<ServiceResponse<Board>>> GetBoardByNameAsync(string boardName)
        {
            var response = await _boardService.GetBoardByNameAsync(boardName);
            return Ok(response);
        }

        [HttpGet("get/guid/{guid}")]
        public async Task<ActionResult<ServiceResponse<Board>>> GetBoardByGuidAsync(Guid guid)
        {
            var response = await _boardService.GetBoardByGuidAsync(guid);
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<ActionResult<ServiceResponse<List<Board>>>> GetBoardsAsync()
        {
            var response = await _boardService.GetBoardsAsync();
            return Ok(response);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ServiceResponse<Board>>> AddBoardAsync(BoardNew board)
        {
            var response = await _boardService.AddBoardAsync(board);
            return Ok(response);
        }
    }
}
