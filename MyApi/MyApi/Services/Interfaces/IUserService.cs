using MyApi.DTOs.CommonDtos;
using MyApi.DTOs.UserDtos;

namespace MyApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto> CreateUserAsync(UserCreateDto userCreateDto);
        Task<UserGetDto> GetUserInfo(string userName);
        Task<ResponseDto> UpdateUserInfo(string userName, UserPutDto userPutDto);
    }
}
