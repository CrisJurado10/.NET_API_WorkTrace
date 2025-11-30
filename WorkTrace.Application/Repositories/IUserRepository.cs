using WorkTrace.Data.Models;

namespace WorkTrace.Application.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<List<User>> GetByDocumentNumberAndEmailAsync(string documentNumber, string email);
    Task<User?> GetByEmailAsync(string email);
}