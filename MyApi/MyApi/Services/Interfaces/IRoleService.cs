using MyApi.DTOs.RoleDtos;

namespace MyApi.Services.Interfaces;

public interface IRoleService
{
    public Task<GetRolesDto> GetAllRoles { get; set; }
}
