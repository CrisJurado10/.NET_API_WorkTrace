using AutoMapper;
using MongoDB.Bson;
using WorkTrace.Application.DTOs.AssignmentDTO.Management;
using WorkTrace.Application.DTOs.AssignmentDTO.Mobile;
using WorkTrace.Data.Models;

namespace WorkTrace.Application.Profiles;

public class AssignmentProfile : Profile
{

    public AssignmentProfile()
    {
        CreateMap<CreateAssignmentRequest, Assignment>()
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => ObjectId.Parse(src.Client)))
            .ForMember(dest => dest.Service, opt => opt.MapFrom(src => ObjectId.Parse(src.Service)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ObjectId.Parse(src.Status)))
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => ObjectId.Parse(src.CreatedByUser)))
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Select(ObjectId.Parse).ToList()))
            .ForMember(dest => dest.DestinationLocation, opt => opt.Ignore());

        CreateMap<UpdateAssignmentMobileRequest, Assignment>()
            .ForMember(dest => dest.CheckIn, opt => opt.Condition(src => src.CheckIn.HasValue))
            .ForMember(dest => dest.CheckOut, opt => opt.Condition(src => src.CheckOut.HasValue))
            .ForMember(dest => dest.CurrentLocation, opt => opt.Condition(src => src.CurrentLocation != null))
            .ForMember(dest => dest.Comment, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Comment)))
            //.ForMember(dest => dest.StepsProgress, opt => opt.Condition(src => src.StepsProgress != null && src.StepsProgress.Any()))
            .ForMember(dest => dest.MediaFiles, opt => opt.Condition(src => src.MediaFiles != null && src.MediaFiles.Any()));

        CreateMap<UpdateAssignmentWebRequest, Assignment>()
            .ForMember(dest => dest.AssignedDate, opt => opt.Condition(src => src.AssignedDate.HasValue))
            .ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(src => src.AssignedDate.Value))
            .ForMember(dest => dest.Client, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Client)))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => ObjectId.Parse(src.Client)))
            .ForMember(dest => dest.Service, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Service)))
            .ForMember(dest => dest.Service, opt => opt.MapFrom(src => ObjectId.Parse(src.Service)))
            .ForMember(dest => dest.Status, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Status)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ObjectId.Parse(src.Status)))
            .ForMember(dest => dest.CreatedByUser, opt => opt.Condition(src => !string.IsNullOrEmpty(src.CreatedByUser)))
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => ObjectId.Parse(src.CreatedByUser)))
            .ForMember(dest => dest.Users, opt => opt.Condition(src => src.Users != null && src.Users.Any()))
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Select(ObjectId.Parse).ToList()))
            .ForMember(dest => dest.Address, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Address)))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<Assignment, AssignmentResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Select(u => u.ToString()).ToList()))
            .ForMember(dest => dest.Service, opt => opt.MapFrom(src => src.Service.ToString()))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(src =>
                src.AssignedDate.ToString("dd-MM-yyyy")
            ))
            .ForMember(dest => dest.AssignedTime, opt => opt.MapFrom(src =>
                src.AssignedDate.ToString("HH:mm")
            ));

        CreateMap<Assignment, AssignmentMobileResponse>()
            .ForMember(dest => dest.Service, opt => opt.MapFrom(src => src.Service.ToString()))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AssignedDate,
                opt => opt.MapFrom(src => src.AssignedDate))
            .ForMember(dest => dest.CheckIn,
                opt => opt.MapFrom(src => src.CheckIn))
            .ForMember(dest => dest.CheckOut,
                opt => opt.MapFrom(src => src.CheckOut));

        CreateMap<Assignment, AssigmentMobileDashboardResponse>()
           .ForMember(dest => dest.AssignedDate,
               opt => opt.MapFrom(src => src.AssignedDate           
                   .ToString("dd-MM-yyyy")))
           .ForMember(dest => dest.AssignedTime,
               opt => opt.MapFrom(src => src.AssignedDate
                   .ToString("HH:mm")));

        CreateMap<Assignment, AssignmentTrackingResponse>()
            .ForMember(dest => dest.CheckInDate,
                opt => opt.MapFrom(src => src.CheckIn.HasValue
                    ? src.CheckIn.Value.ToString("dd-MM-yyyy")
                    : null))
            .ForMember(dest => dest.CheckInTime,
                opt => opt.MapFrom(src => src.CheckIn.HasValue
                    ? src.CheckIn.Value.ToString("HH:mm")
                    : null))
            .ForMember(dest => dest.CheckOutDate,
                opt => opt.MapFrom(src => src.CheckOut.HasValue
                    ? src.CheckOut.Value.ToString("dd-MM-yyyy")
                    : null))
            .ForMember(dest => dest.CheckOutTime,
                opt => opt.MapFrom(src => src.CheckOut.HasValue
                    ? src.CheckOut.Value.ToString("HH:mm")
                    : null));

        CreateMap<Assignment, AssignmentListResponse>()
             .ForMember(dest => dest.AssignedDate,
                 opt => opt.MapFrom(src => src.AssignedDate                     
                     .ToString("dd-MM-yyyy")))
             .ForMember(dest => dest.AssignedTime,
                 opt => opt.MapFrom(src => src.AssignedDate         
                     .ToString("HH:mm")));
    }
}