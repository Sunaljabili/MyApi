using MyApi.DTOs.AuthDtos;

namespace MyApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDto> LoginAsync(LoginDto loginDto);
    }
}
