using AutoMapper;
using WorkTrace.Application.DTOs.ClientDTO.Information;
using WorkTrace.Data.Models;

namespace WorkTrace.Application.Profiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<CreateClientRequest, Client>();
        CreateMap<UpdateClientRequest, Client>();
        CreateMap<Client, ClientInformationResponse>();
    }
}