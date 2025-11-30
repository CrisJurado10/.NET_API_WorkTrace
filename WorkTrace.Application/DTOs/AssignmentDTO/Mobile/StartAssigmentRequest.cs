using WorkTrace.Data.Models;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class StartAssigmentRequest
{
    public DateTime CheckIn { get; set; }
    public GeoPoint CurrentLocation { get; set; }
}