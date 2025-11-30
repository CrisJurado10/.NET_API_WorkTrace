using Microsoft.Extensions.DependencyInjection;
using WorkTrace.Application.Services;
using WorkTrace.Logic.Services;

namespace WorkTrace.Logic;

public static class ServiceExtension
{
    public static void AddLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IServiceandInstallationService, ServiceandInstallationService>();
        services.AddScoped<IStatusService,  StatusService>();
        services.AddScoped<IAssignmentService, AssignmentService>();
        services.AddScoped<IGeocodingService, GeocodingService>();
        services.AddScoped<IFileService, FileService>();
    }
}