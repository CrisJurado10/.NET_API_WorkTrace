namespace WorkTrace.Application.DTOs.ServiceMgmtDTO.Management;

public class ServiceInformationResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<InstallationStepResponse> InstallationSteps { get; set; }
}