using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;

namespace Persistence;

public static class Di
{
    public static IServiceCollection AddPersistense(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWork, EfUnitOfWork>();

        return services;
    }

    public static IServiceCollection AddPersistense(this IServiceCollection services,
        DbContextOptions options)
    {
        services
            .AddPersistense()
            .AddSingleton(new ApplicationDbContext((DbContextOptions<ApplicationDbContext>)options));

        return services;
    }
}