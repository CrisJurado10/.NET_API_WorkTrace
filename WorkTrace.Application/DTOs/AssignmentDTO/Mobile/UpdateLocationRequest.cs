using WorkTrace.Data.Models;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class UpdateLocationRequest
{
    public GeoPoint CurrentLocation { get; set; }
}