using AutoMapper;
using MongoDB.Bson;
using WorkTrace.Application.DTOs.ServiceMgmtDTO.Management;
using WorkTrace.Application.Repositories;
using WorkTrace.Application.Services;
using WorkTrace.Data.Models;

namespace WorkTrace.Logic.Services;

public class ServiceandInstallationService(IInstallationStepRepository _installationStepRepository, IServiceRepository _serviceRepository, IMapper _mapper) : IServiceandInstallationService
{
    public async Task<List<ServiceInformationResponse>> GetAllAsync()
    {
        var services = await _serviceRepository.GetAsync();
        var responses = new List<ServiceInformationResponse>();

        foreach (var service in services)
        {
            var response = _mapper.Map<ServiceInformationResponse>(service);
            response.InstallationSteps = new List<InstallationStepResponse>();

            foreach (var stepId in service.InstallationSteps)
            {
                var step = await _installationStepRepository.GetAsync(stepId.ToString());
                if (step != null)
                    response.InstallationSteps.Add(_mapper.Map<InstallationStepResponse>(step));
            }
            responses.Add(response);
        }

        return responses;
    }
    public async Task<ServiceInformationResponse> GetByIdAsync(string id)
    {
        var serviceById = await _serviceRepository.GetAsync(id);
        if (serviceById == null)
            throw new Exception("Servicio no encontrado.");

        var response = _mapper.Map<ServiceInformationResponse>(serviceById);
        response.InstallationSteps = new List<InstallationStepResponse>();

        // Traer los pasos asociados por ID
        foreach (var stepId in serviceById.InstallationSteps)
        {
            var step = await _installationStepRepository.GetAsync(stepId.ToString());
            if (step != null)
                response.InstallationSteps.Add(_mapper.Map<InstallationStepResponse>(step));
        }

        return response;
    }


    public async Task<ServiceInformationResponse> CreateServiceWithStepAsync(CreateServiceRequest request)
    {
        var installationSteps = new List<InstallationStep>();

        foreach (var stepRequest in request.InstallationSteps)
        {
            var step = _mapper.Map<InstallationStep>(stepRequest);
            await _installationStepRepository.CreateAsync(step);
            installationSteps.Add(step);
        }
        var stepsIds = installationSteps.Select(s => new ObjectId(s.Id)).ToList();

        var service = new Service
        {
            Name = request.Name,
            Description = request.Description,
            InstallationSteps = stepsIds
        };
        await _serviceRepository.CreateAsync(service);

        var response = _mapper.Map<ServiceInformationResponse>(service);
        return response;
    }

    public async Task<ServiceInformationResponse> UpdateServiceAsync(string id, UpdateServiceRequest request)
    {
        var existingService = await _serviceRepository.GetAsync(id);
        if (existingService == null)
            throw new Exception("Servicio no encontrado.");

        existingService.Name = request.Name;
        existingService.Description = request.Description;

        var updatedStepIds = new List<ObjectId>();

        foreach (var stepRequest in request.InstallationSteps)
        {
            if (!string.IsNullOrEmpty(stepRequest.Id))
            {
                var step = await _installationStepRepository.GetAsync(stepRequest.Id);
                if (step != null)
                {
                    step.Steps = stepRequest.Steps;
                    step.Description = stepRequest.Description;
                    await _installationStepRepository.UpdateAsync(step.Id, step);
                    updatedStepIds.Add(new ObjectId(step.Id));
                }
            }
            else
            {
                var newStep = _mapper.Map<InstallationStep>(stepRequest);
                await _installationStepRepository.CreateAsync(newStep);
                updatedStepIds.Add(new ObjectId(newStep.Id));
            }
        }

        existingService.InstallationSteps = updatedStepIds;
        await _serviceRepository.UpdateAsync(existingService.Id, existingService);

        var response = _mapper.Map<ServiceInformationResponse>(existingService);
        response.InstallationSteps = new List<InstallationStepResponse>();

        foreach (var stepId in updatedStepIds)
        {
            var step = await _installationStepRepository.GetAsync(stepId.ToString());
            if (step != null)
                response.InstallationSteps.Add(_mapper.Map<InstallationStepResponse>(step));
        }

        return response;
    }
}