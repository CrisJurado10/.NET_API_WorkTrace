using MongoDB.Bson;
using WorkTrace.Data.Common.Generics;

namespace WorkTrace.Data.Models;

public class Service : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ObjectId> InstallationSteps { get; set; }
}