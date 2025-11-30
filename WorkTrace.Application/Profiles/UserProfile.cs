using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using WorkTrace.Application.DTOs.UserDTO.Information;
using WorkTrace.Data.Models;

namespace WorkTrace.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<LoginRequest, User>();
        CreateMap<UpdateUserRequest, User>();
        CreateMap<User, UserInformationResponse>();
    }
}