using WorkTrace.Data.Common.Generics;

namespace WorkTrace.Data.Models;

public class Client : BaseModel
{
    public string FullName { get; set; }
    public string DocumentNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}