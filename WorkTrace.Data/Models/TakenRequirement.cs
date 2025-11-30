using MongoDB.Bson;
using WorkTrace.Data.Common.Generics;

namespace WorkTrace.Data.Models;

public class TakenRequirement : BaseModel 
{
    public ObjectId User { get; set; }
    public ObjectId Client {  get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}