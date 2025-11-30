using AutoMapper;
using WorkTrace.Application.DTOs.StatusDTO.Information;
using WorkTrace.Data.Models;

namespace WorkTrace.Application.Profiles;

public class StatusProfile : Profile
{
    public StatusProfile()
    {
        CreateMap<CreateStatusRequest, Status>();
        CreateMap<UpdateStatusRequest, Status>();
        CreateMap<Status, StatusInformationResponse>();
    }
}