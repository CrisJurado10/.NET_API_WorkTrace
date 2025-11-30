namespace WorkTrace.Application.DTOs.ServiceMgmtDTO.Management;

public class CreateServiceRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<InstallationStepResponse> InstallationSteps { get; set; }
}
