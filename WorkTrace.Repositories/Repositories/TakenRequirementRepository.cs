using WorkTrace.Application.Repositories;
using WorkTrace.Data;
using WorkTrace.Data.Models;

namespace WorkTrace.Repositories.Repositories;

public class TakenRequirementRepository : GenericRepository<TakenRequirement>, ITakenRequirementRepository
{
    public TakenRequirementRepository(WorkTraceContext context)
    {
        Collection = context.GetCollection<TakenRequirement>("takenRequirements");
    }
}