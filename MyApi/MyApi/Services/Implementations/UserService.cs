using Microsoft.AspNetCore.Identity;
using MyApi.Contexts;
using MyApi.DTOs.CommonDtos;
using MyApi.DTOs.UserDtos;
using MyApi.Exceptions.UserExceptions;
using MyApi.Models;
using MyApi.Services.Interfaces;
using MyApi.Utils;
using System.Net;

namespace MyApi.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<ResponseDto> CreateUserAsync(UserCreateDto userCreateDto)
    {
        AppUser newUser = new AppUser
        {
            UserName = userCreateDto.UserName,
            Email = userCreateDto.Email,
        };

        IdentityResult result = await _userManager.CreateAsync(newUser, userCreateDto.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());
            return new()
            {
                Message = "User successfully created",
                StatusCode = (int)HttpStatusCode.Created
            };
        }


        throw new UserCreateFailedException(result.Errors);
    }

    public async Task<UserGetDto> GetUserInfo(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            throw new UserNotFoundException();

        return new UserGetDto
        {
            UserName = userName,
            Email = user.Email,
        };
    }

    public async Task<ResponseDto> UpdateUserInfo(string userName, UserPutDto userPutDto)
    {
        var loginedUserName = _contextAccessor.HttpContext.User.Identity.Name;
        if(userName != loginedUserName)
            throw new UserCannotAccessToUpdateException();

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            throw new UserNotFoundException();

        user.UserName = userPutDto.UserName;
        user.Email = userPutDto.Email;

        IdentityResult identityResult = await _userManager.UpdateAsync(user);
        if (!identityResult.Succeeded)
            throw new UserCreateFailedException(identityResult.Errors);

        return new ResponseDto
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "User successfully updated"
        };
    }
}