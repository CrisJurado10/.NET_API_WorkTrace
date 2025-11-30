using AutoMapper;
using WorkTrace.Application.DTOs.StatusDTO.Information;
using WorkTrace.Application.Repositories;
using WorkTrace.Application.Services;
using WorkTrace.Data.Models;

namespace WorkTrace.Logic.Services;

public class StatusService(IStatusRepository _statusRepository, IMapper _mapper) : IStatusService
{
    public async Task<List<StatusInformationResponse>> GetAllAsync()
    {
        var systemStatus = await _statusRepository.GetAsync();
        return _mapper.Map<List<StatusInformationResponse>>(systemStatus);
    }

    public async Task<StatusInformationResponse> GeyByIdAsync(string id)
    {
        var statusById = await _statusRepository.GetAsync(id) ?? throw new Exception("Status no Encontrado");
        var response = _mapper.Map<StatusInformationResponse>(statusById);
        return response;
    }

    public async Task<StatusInformationResponse> CreateStatusAsync(CreateStatusRequest statusCreate)
    {
        var existingStatus = await _statusRepository.GetByName(statusCreate.Name);
        if (existingStatus != null) throw new Exception("Ya hay un Status con este nombre");

        var statusToDatabase = _mapper.Map<Status>(statusCreate);

        await _statusRepository.CreateAsync(statusToDatabase);
        return _mapper.Map<StatusInformationResponse>(statusToDatabase);
    }

    public async Task<StatusInformationResponse> UpdateStatusAsync(string id, UpdateStatusRequest status)
    {
        var statusToUpdate = await _statusRepository.GetAsync(id);
        if (statusToUpdate == null) throw new Exception("Status no Encontrado");

        statusToUpdate.Name = string.IsNullOrWhiteSpace(status.Name) ? statusToUpdate.Name : status.Name;
        statusToUpdate.Description = string.IsNullOrWhiteSpace(status.Description) ? statusToUpdate.Description : status.Description;
        if (status.IsActive.HasValue) statusToUpdate.IsActive = status.IsActive.Value;

        await _statusRepository.UpdateAsync(id, statusToUpdate);

        return _mapper.Map<StatusInformationResponse>(statusToUpdate);
    }

    public async Task<bool> SetStatusInactive(string statusId)
    {
        var statusFilter = await _statusRepository.GetAsync(statusId);
        if (statusFilter == null) throw new Exception("Status no Encontrado");

        if (!statusFilter.IsActive) return false;

        statusFilter.IsActive = false;

        await _statusRepository.UpdateAsync(statusId, statusFilter);

        return true;
    }
}