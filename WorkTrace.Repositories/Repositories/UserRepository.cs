using MongoDB.Driver;
using WorkTrace.Application.Repositories;
using WorkTrace.Data;
using WorkTrace.Data.Models;

namespace WorkTrace.Repositories.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(WorkTraceContext context)
    {
        Collection = context.GetCollection<User>("users");
    }

    public async Task<List<User>> GetByDocumentNumberAndEmailAsync(string documentNumber, string email) =>
        await Collection.Find(x => x.DocumentNumber == documentNumber || x.Email == email).ToListAsync();

    public async Task<User?> GetByEmailAsync(string email) =>
        await Collection.Find(x => x.Email == email).FirstOrDefaultAsync();
}