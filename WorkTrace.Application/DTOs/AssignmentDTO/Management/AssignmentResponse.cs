using WorkTrace.Data.Models;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Management;
public class AssignmentResponse
{
    public string Id { get; set; }
    public List<string> Users { get; set; }
    public string Service { get; set; }
    public string Client { get; set; }
    public string Status { get; set; }
    public string AssignedDate { get; set; }
    public string AssignedTime { get; set; }
    public string Address { get; set; }
    public GeoPoint? DestinationLocation { get; set; }
    public string CreatedByUser { get; set; }
}