using WorkTrace.Data.Common.Generics;

namespace WorkTrace.Data.Models;

public class Status : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}