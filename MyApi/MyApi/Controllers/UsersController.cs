using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs.CommonDtos;
using MyApi.DTOs.UserDtos;
using MyApi.Services.Interfaces;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var response = await _userService.CreateUserAsync(userCreateDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{userName}")]
        [Authorize]
        public async Task<IActionResult> GetUserInfoAsync(string userName)
        {
            return Ok(await _userService.GetUserInfo(userName));
        }

        [HttpPut("{existUserName}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] string existUserName, [FromBody] UserPutDto userPutDto)
        {
            var response = await _userService.UpdateUserInfo(existUserName, userPutDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
