using WorkTrace.Application.DTOs.UserDTO.Information;
using WorkTrace.Application.DTOs.UserDTO.Login;

namespace WorkTrace.Application.Services;

public interface IUserService
{
    Task<List<UserInformationResponse>> GetAllAsync();
    Task<UserInformationResponse> GetByIdAsync(string id);
    Task<UserInformationResponse> CreateAsync(CreateUserRequest userCreate);
    Task<LoginResponse> LoginAsync(string email, string password);
    Task<UserInformationResponse> UpdateAsync(string id, UpdateUserRequest user);
    Task<bool> SetInactiveUser(string userId);
}