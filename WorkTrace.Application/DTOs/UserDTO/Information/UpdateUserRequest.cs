namespace WorkTrace.Application.DTOs.UserDTO.Information;

public class UpdateUserRequest
{
    public string? FullName { get; set; }
    public string? DocumentNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public Enums.UserRoles? Role { get; set; }
    public bool? IsActive { get; set; }
}