using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkTrace.Data.Common.Generics;

public class BaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}