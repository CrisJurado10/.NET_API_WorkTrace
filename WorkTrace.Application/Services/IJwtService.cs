using WorkTrace.Data.Models;

namespace WorkTrace.Application.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}
