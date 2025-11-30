using WorkTrace.Data.Common.Generics;

namespace WorkTrace.Data.Models;

public class InstallationStep : BaseModel
{
    public int Steps { get; set; }
    public string Description { get; set; }
}