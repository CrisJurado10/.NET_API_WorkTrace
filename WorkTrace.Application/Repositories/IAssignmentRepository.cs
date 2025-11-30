using MongoDB.Bson;
using WorkTrace.Data.Models;

namespace WorkTrace.Application.Repositories;

public interface IAssignmentRepository : IGenericRepository<Assignment>
{
    Task<List<BsonDocument>> GetClientAssignmentRawAsync(string clientId);
    Task<List<Assignment>> GetAssignmentByUserAndDateRangeAsync(string userId, DateTime start, DateTime end);
    Task<List<BsonDocument>> GetAssignmentsListByUserRawAsync(string userId);
    Task<BsonDocument?> GetAssignmentTrackingRawAsync(string assignmentId);
}