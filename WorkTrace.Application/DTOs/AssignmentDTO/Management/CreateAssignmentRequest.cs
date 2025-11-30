namespace WorkTrace.Application.DTOs.AssignmentDTO.Management;

public class CreateAssignmentRequest
{
    public List<string> Users { get; set; } = new();
    public string Service { get; set; }
    public string Client { get; set; }
    public string Status { get; set; }
    public DateTime AssignedDate { get; set; }
    public string Address { get; set; }
    public string CreatedByUser { get; set; }
}