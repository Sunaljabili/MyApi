using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs.AuthDtos;
using MyApi.Services.Interfaces;
using MyApi.Utils;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(IAuthService authService, RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _roleManager = roleManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            return Ok(response);
        }

        //[HttpPost("create-roles")]
        //public async Task<IActionResult> CreateRolesAsync()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Member.ToString() });

        //    return Ok("Success");
        //}
    }
}
