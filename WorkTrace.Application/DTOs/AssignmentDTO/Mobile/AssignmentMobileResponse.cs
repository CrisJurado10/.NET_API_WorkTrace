using WorkTrace.Data.Models;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class AssignmentMobileResponse
{
    public string Id { get; set; }
    public string Service { get; set; }
    public string Client { get; set; }
    public string Status { get; set; }

    public DateTime AssignedDate { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }

    public string Address { get; set; }

    public GeoPoint? CurrentLocation { get; set; }
    public GeoPoint? DestinationLocation { get; set; }

    //public List<StepProgress> StepsProgress { get; set; }
    public List<MediaFile> MediaFiles { get; set; }

    public string Comment { get; set; }
}