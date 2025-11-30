using MongoDB.Bson;
using MongoDB.Driver;
using WorkTrace.Application.Repositories;
using WorkTrace.Data;
using WorkTrace.Data.Models;

namespace WorkTrace.Repositories.Repositories;

public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
{
    public AssignmentRepository(WorkTraceContext context)
    {
        Collection = context.GetCollection<Assignment>("assignments");
    }

    public async Task<List<BsonDocument>> GetClientAssignmentRawAsync(string clientId)
    {
        var clientObjectId = new ObjectId(clientId);

        var pipeline = Collection.Aggregate()
            .Match(a => a.Client == clientObjectId)
            .SortByDescending(a => a.AssignedDate)
            .Lookup("services", "Service", "_id", "ServiceInfo")
            .Lookup("status", "Status", "_id", "StatusInfo")
            .Lookup("users", "Users", "_id", "UserInfo")
            .Project(new BsonDocument
            {

                { "Service", new BsonDocument("$arrayElemAt", new BsonArray { "$ServiceInfo.Name", 0 }) },
                { "AssignedDate", "$AssignedDate" },
                { "CheckOut", "$CheckOut" },
                { "Status", new BsonDocument("$arrayElemAt", new BsonArray { "$StatusInfo.Name", 0 }) },
                { "Address", "$Address" },
                { "Users", "$UserInfo.FullName" }
            });
        return await pipeline.ToListAsync();
    }

    public async Task<List<Assignment>> GetAssignmentByUserAndDateRangeAsync(string userId, DateTime start, DateTime end)
    {
        var userObjectId = new ObjectId(userId);

        var filter = Builders<Assignment>.Filter.And(
            Builders<Assignment>.Filter.AnyEq(a => a.Users, userObjectId),
            Builders<Assignment>.Filter.Gte(a => a.AssignedDate, start),
            Builders<Assignment>.Filter.Lte(a => a.AssignedDate, end)
        );
        return await Collection.Find(filter).ToListAsync();
    }

    public async Task<List<BsonDocument>> GetAssignmentsListByUserRawAsync(string userId)
    {
        var userObjectId = new ObjectId(userId);
        var now = DateTime.UtcNow;

        var pipeline = new[]
        {
        new BsonDocument("$match", new BsonDocument("Users", userObjectId)),

        new BsonDocument("$addFields",
            new BsonDocument("Distance",
                new BsonDocument("$abs",
                    new BsonDocument("$subtract", new BsonArray { "$AssignedDate", now })
                )
            )
        ),

        new BsonDocument("$lookup",
            new BsonDocument
            {
                { "from", "clients" },
                { "localField", "Client" },
                { "foreignField", "_id" },
                { "as", "ClientInfo" }
            }
        ),
        new BsonDocument("$unwind",
            new BsonDocument
            {
                { "path", "$ClientInfo" },
                { "preserveNullAndEmptyArrays", true }
            }
        ),

        new BsonDocument("$lookup",
            new BsonDocument
            {
                { "from", "services" },
                { "localField", "Service" },
                { "foreignField", "_id" },
                { "as", "ServiceInfo" }
            }
        ),
        new BsonDocument("$unwind",
            new BsonDocument
            {
                { "path", "$ServiceInfo" },
                { "preserveNullAndEmptyArrays", true }
            }
        ),

        new BsonDocument("$project",
            new BsonDocument
            {
                { "_id", 1 },
                { "Client", new BsonDocument("$ifNull", new BsonArray { "$ClientInfo.FullName", "" }) },
                { "Service", new BsonDocument("$ifNull", new BsonArray { "$ServiceInfo.Name", "" }) },
                { "AssignedDate", 1 },
                { "Distance", 1 }
            }
        ),

        new BsonDocument("$sort", new BsonDocument { { "Distance", 1 } })
    };

        return await Collection.Aggregate<BsonDocument>(pipeline).ToListAsync();
    }


    public async Task<BsonDocument?> GetAssignmentTrackingRawAsync(string assignmentId)
    {
        var id = new ObjectId(assignmentId);

        var pipeline = new[]
        {
        new BsonDocument("$match",
            new BsonDocument("_id", id)
        ),

        new BsonDocument("$lookup",
            new BsonDocument
            {
                { "from", "clients" },
                { "localField", "Client" },
                { "foreignField", "_id" },
                { "as", "ClientInfo" }
            }
        ),

        new BsonDocument("$lookup",
            new BsonDocument
            {
                { "from", "services" },
                { "localField", "Service" },
                { "foreignField", "_id" },
                { "as", "ServiceInfo" }
            }
        ),

        new BsonDocument("$project",
            new BsonDocument
            {
                { "_id", 1 },
                { "Client", new BsonDocument("$arrayElemAt", new BsonArray { "$ClientInfo.FullName", 0 }) },
                { "Service", new BsonDocument("$arrayElemAt", new BsonArray { "$ServiceInfo.Name", 0 }) },
                { "Address", 1 },
                { "CheckIn", 1 },
                { "CheckOut", 1 },
                { "CurrentLocation", 1 },
                { "DestinationLocation", 1 }
            }
        )
    };

        return await Collection.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
    }
}