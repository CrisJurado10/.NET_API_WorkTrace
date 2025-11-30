using WorkTrace.Data.Models;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class UpdateAssignmentMobileRequest
{
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public GeoPoint? CurrentLocation { get; set; }
    //public List<StepProgress> StepsProgress { get; set; }
    public List<MediaFile> MediaFiles { get; set; }
    public string Comment { get; set; }
}