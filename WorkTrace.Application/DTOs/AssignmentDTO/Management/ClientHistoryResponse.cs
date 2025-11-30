namespace WorkTrace.Application.DTOs.AssignmentDTO.Management;

public class ClientHistoryResponse
{
    public string Service { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public string CheckOutDate { get; set; }
    public string CheckOutTime { get; set; }
    public string Status { get; set; }
    public string Address { get; set; }
    public List<string> Users { get; set; }
}