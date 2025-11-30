namespace WorkTrace.Application.DTOs.ServiceMgmtDTO.Management;

public class CreateInstallationStepRequest
{
    public required int Steps { get; set; }
    public required string Description { get; set; }
}