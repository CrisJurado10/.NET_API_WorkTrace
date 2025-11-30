using WorkTrace.Data.Common.Generics;

namespace WorkTrace.Application.Repositories;

public interface IGenericRepository<T> where T : BaseModel
{
    Task<List<T>> GetAsync();
    Task<T?> GetAsync(string id);
    Task CreateAsync(T model);
    Task UpdateAsync(string id, T model);
    Task RemoveAsync(string id);
}