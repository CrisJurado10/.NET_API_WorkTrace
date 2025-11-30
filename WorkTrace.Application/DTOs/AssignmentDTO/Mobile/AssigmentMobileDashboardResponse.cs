namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class AssigmentMobileDashboardResponse
{
    public string Id { get; set; }
    public string Client { get; set; }
    public string Service { get; set; }
    public string Status { get; set; }
    public string Address { get; set; }
    public string AssignedDate { get; set; }
    public string AssignedTime { get; set; }
    public string CreatedByUser { get; set; }
}