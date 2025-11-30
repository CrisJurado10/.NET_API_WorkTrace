namespace WorkTrace.Application.DTOs.ServiceMgmtDTO.Management;

public class UpdateServiceRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<UpdateInstallationStepRequest> InstallationSteps { get; set; }
}