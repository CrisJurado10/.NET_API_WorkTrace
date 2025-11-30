using WorkTrace.Application.Repositories;
using WorkTrace.Data;
using WorkTrace.Data.Models;

namespace WorkTrace.Repositories.Repositories;

public class InstallationStepRepository : GenericRepository<InstallationStep>, IInstallationStepRepository
{
    public InstallationStepRepository(WorkTraceContext context)
    {
        Collection = context.GetCollection<InstallationStep>("installationSteps");
    }
}