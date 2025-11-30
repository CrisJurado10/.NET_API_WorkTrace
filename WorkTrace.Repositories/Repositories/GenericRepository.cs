using MongoDB.Driver;
using WorkTrace.Application.Repositories;
using WorkTrace.Data.Common.Generics;

namespace WorkTrace.Repositories.Repositories;

public abstract class GenericRepository<T>() : IGenericRepository<T> where T : BaseModel
{
    public required IMongoCollection<T> Collection;
    public async Task<List<T>> GetAsync() =>
        await Collection.Find(_ => true).ToListAsync();

    public async Task<T?> GetAsync(string id) =>
        await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(T model) =>
        await Collection.InsertOneAsync(model);

    public async Task UpdateAsync(string id, T model) =>
        await Collection.ReplaceOneAsync(x => x.Id == id, model);

    public async Task RemoveAsync(string id) =>
        await Collection.DeleteOneAsync(x => x.Id == id);
}
