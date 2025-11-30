using WorkTrace.Application.DTOs.ClientDTO.Information;

namespace WorkTrace.Application.Services;

public interface IClientService
{
    Task<List<ClientInformationResponse>> GetAllAsync();
    Task<ClientInformationResponse> GetByIdAsync(string id);
    Task<ClientInformationResponse> CreateClientAsync(CreateClientRequest clientCreate);
    Task<ClientInformationResponse> UpdateClientAsync(string id, UpdateClientRequest client);
}