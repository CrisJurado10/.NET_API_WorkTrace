namespace WorkTrace.Application.DTOs.ServiceMgmtDTO.Management;

public class UpdateInstallationStepRequest
{
    public string? Id { get; set; } 
    public required int Steps { get; set; }
    public required string Description { get; set; }
}