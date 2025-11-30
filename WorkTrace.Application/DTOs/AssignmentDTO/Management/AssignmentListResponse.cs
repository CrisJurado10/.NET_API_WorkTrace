namespace WorkTrace.Application.DTOs.AssignmentDTO.Management;

public class AssignmentListResponse
{
    public string Id { get; set; }
    public string Client { get; set; }
    public string Service { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime AssignedTime { get; set; }
}