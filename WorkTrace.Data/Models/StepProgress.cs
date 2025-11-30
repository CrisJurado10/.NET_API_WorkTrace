using WorkTrace.Data.Common.Generics;

namespace WorkTrace.Data.Models;
public class StepProgress : BaseModel
{
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
}