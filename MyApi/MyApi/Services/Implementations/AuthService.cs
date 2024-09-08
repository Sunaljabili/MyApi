using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyApi.DTOs.AuthDtos;
using MyApi.Exceptions.AuthExceptions;
using MyApi.Models;
using MyApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApi.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user is null)
                throw new AuthenticationFailException();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if (result.Succeeded)
            {
                List<Claim> claims = new()
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken jwtSecurityToken = new(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.UtcNow.AddHours(1),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: signingCredentials,
                    claims: claims
                    );

                JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
                string token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

                return new TokenResponseDto
                {
                    Token = token,
                    Expiration = jwtSecurityToken.ValidTo,
                    UserName = user.UserName
                };
            }

            throw new AuthenticationFailException();
        }
    }
}
