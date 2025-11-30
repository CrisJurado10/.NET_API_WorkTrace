namespace WorkTrace.Application.DTOs.UserDTO.Login;

public class LoginResponse
{
    public string Token { get; set; }
    public DateTime ExpireAt { get; set; }
}