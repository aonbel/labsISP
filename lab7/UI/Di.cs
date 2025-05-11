using UI.Infrastructure;
using UI.Pages;
using UI.ViewModels;

namespace UI;

public static class Di
{
    public static IServiceCollection AddPages(this IServiceCollection services)
    {
        services.AddTransient<ArtistsPageModel>();
        services.AddTransient<ArtistCreationPageModel>();
        
        services.AddTransient<SongDetailsPageModel>();
        services.AddTransient<SongCreationPageModel>();
        services.AddTransient<SongUpdatePageModel>();
        
        return services;
    }
    
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddScoped<ArtistsViewModel>();
        services.AddScoped<ArtistCreationViewModel>();
        
        services.AddScoped<SongDetailsViewModel>();
        services.AddScoped<SongCreationViewModel>();
        services.AddScoped<SongUpdateViewModel>();
        
        return services;
    }

    public static IServiceCollection AddUI(this IServiceCollection services)
    {
        services.AddTransient<ImageHandler>();
        
        return services;
    }
}