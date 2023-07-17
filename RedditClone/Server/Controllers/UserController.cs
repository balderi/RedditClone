using Microsoft.AspNetCore.Mvc;

namespace RedditClone.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get/id/{id:int}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUserByIdAsync(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            return Ok(response);
        }

        [HttpGet("get/guid/{guid}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUserByGuidAsync(Guid guid)
        {
            var response = await _userService.GetUserByGuidAsync(guid);
            return Ok(response);
        }

        [HttpGet("get/email/{email}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUserByEmailAsync(string email)
        {
            var response = await _userService.GetUserByEmailAsync(email);
            return Ok(response);
        }

        [HttpGet("get/name/{name}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUserByNameAsync(string name)
        {
            var response = await _userService.GetUserByNameAsync(name);
            return Ok(response);
        }

        [HttpGet("get/token/{token}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUserByTokenAsync(string token)
        {
            var response = await _userService.GetUserByTokenAsync(token);
            return Ok(response);
        }
    }
}
