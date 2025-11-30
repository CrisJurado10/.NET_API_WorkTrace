using Microsoft.Extensions.DependencyInjection; //Inyeccion de dependencias 

namespace WorkTrace.Data;

public static class ServiceExtension
{
    public static void AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<WorkTraceContext>();
    }
}