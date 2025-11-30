using MongoDB.Bson;
using WorkTrace.Data.Common.Generics;

namespace WorkTrace.Data.Models;

public class Assignment : BaseModel
{
    public List<ObjectId> Users { get; set; }
    public ObjectId Service { get; set; }
    public ObjectId Client { get; set; }
    public ObjectId Status { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public string Address { get; set; }
    public GeoPoint? CurrentLocation { get; set; }
    public GeoPoint? DestinationLocation { get; set; }
    public List<StepProgress> StepsProgress { get; set; }
    public bool AllowStepReversal { get; set; } = false;
    public List<MediaFile> MediaFiles { get; set; }
    public ObjectId CreatedByUser { get; set; }
    public string Comment { get; set; }
}