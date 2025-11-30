using WorkTrace.Application.DTOs.ServiceMgmtDTO.Management;

namespace WorkTrace.Application.Services;

public interface IServiceandInstallationService
{
    Task<List<ServiceInformationResponse>> GetAllAsync();
    Task<ServiceInformationResponse> GetByIdAsync(string id);
    Task<ServiceInformationResponse> CreateServiceWithStepAsync(CreateServiceRequest request);
    Task<ServiceInformationResponse> UpdateServiceAsync(string id, UpdateServiceRequest request);
}