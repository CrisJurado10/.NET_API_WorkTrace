using MongoDB.Bson.Serialization.Attributes;
using WorkTrace.Application.Enums;
using WorkTrace.Data.Common.Generics;

namespace WorkTrace.Data.Models;

public class User : BaseModel
{
    public string FullName { get; set; }
    public string DocumentNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public UserRoles Role { get; set; }
    public bool IsActive { get; set; }
}