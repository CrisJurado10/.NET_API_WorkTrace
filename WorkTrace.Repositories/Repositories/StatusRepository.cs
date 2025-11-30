using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using WorkTrace.Application.Repositories;
using WorkTrace.Data;
using WorkTrace.Data.Models;

namespace WorkTrace.Repositories.Repositories;

public class StatusRepository : GenericRepository<Status>, IStatusRepository
{
    public StatusRepository(WorkTraceContext context)
    {
        Collection = context.GetCollection<Status>("status");
    }

    public async Task<Status?> GetByName(string name)
    {
        var trimmedName = name.Trim();

        var filter = Builders<Status>.Filter.Regex(
            s => s.Name,
            new BsonRegularExpression($"^{Regex.Escape(trimmedName)}$", "i")
        );

        return await Collection.Find(filter).FirstOrDefaultAsync();
    }
}