using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WorkTrace.Data.Common.Setttings;

namespace WorkTrace.Data;

public class WorkTraceContext
{        
    private readonly IMongoDatabase _database;

    public WorkTraceContext(IOptions<WorkTraceDatabaseSettings> workTraceDatabaseSettings) 
    {
       var mongoClient = new MongoClient(workTraceDatabaseSettings.Value.ConnectionString); //MongoClient

       _database = mongoClient.GetDatabase(workTraceDatabaseSettings.Value.DataBaseName); //Cual Base de datos
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName) 
    {
        return _database.GetCollection<T>(collectionName);  
    }
}
