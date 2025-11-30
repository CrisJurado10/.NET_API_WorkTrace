using WorkTrace.Data.Models;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Management;

public class AssignmentTrackingResponse
{
    public string Id { get; set; }
    public string Client { get; set; }
    public string Service { get; set; }
    public string Address { get; set; }
    public string? CheckInDate { get; set; }
    public string? CheckInTime { get; set; }
    public string? CheckOutDate { get; set; }
    public string? CheckOutTime { get; set; }
    public GeoPoint? CurrentLocation { get; set; }
    public GeoPoint? DestinationLocation { get; set; }
}