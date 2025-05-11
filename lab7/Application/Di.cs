using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Di
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(Di).Assembly));
        
        return services;
    }
}