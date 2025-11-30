namespace WorkTrace.Application.DTOs.StatusDTO.Information;

public class CreateStatusRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}