using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkTrace.Application.DTOs.UserDTO.Information;
using WorkTrace.Application.DTOs.UserDTO.Login;
using WorkTrace.Application.Services;

namespace WorkTrace.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<UserInformationResponse>>> GetAll()
    {
        var usersInSystem = await userService.GetAllAsync();
        return Ok(usersInSystem);
    }

    [Authorize]
    [HttpGet]
    public async Task <UserInformationResponse> GetById(string id) =>
        await userService.GetByIdAsync(id);

    [Authorize]
    [HttpPost]
    public async Task<UserInformationResponse> Create(CreateUserRequest user) =>
        await userService.CreateAsync(user);

    [HttpPost]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var loginUser = await userService.LoginAsync(request.Email, request.Password);
        return Ok(loginUser);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<UserInformationResponse>> Update(string id, [FromBody] UpdateUserRequest request)
    {
        var userUpdate = await userService.UpdateAsync(id, request);
        return Ok(userUpdate);
    }

    [Authorize]
    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> DeactivateUser(string id)
    {
        var succes = await userService.SetInactiveUser(id);
        if (!succes) return NotFound("Usuario no encontrado o Inactivo");
        return Ok("Ahora Usuario esta Inactivo");
    }
}