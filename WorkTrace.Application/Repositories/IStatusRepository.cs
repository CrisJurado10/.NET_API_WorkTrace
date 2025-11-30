using WorkTrace.Data.Models;

namespace WorkTrace.Application.Repositories;

public interface IStatusRepository : IGenericRepository<Status>
{
    Task<Status?> GetByName(string name);
}