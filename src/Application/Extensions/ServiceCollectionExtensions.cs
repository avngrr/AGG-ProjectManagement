using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationMappings(this IServiceCollection services)
    {
        return services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}