using MauiLabs.Lab5.Services;
using MauiLabs.Lab5.Services.Interfaces;
using MauiLabs.Lab6.Services;
using MauiLabs.Lab6.Services.Interfaces;

namespace MauiLabs;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services.AddHttpClient<IRateService, RateService>(client => 
            client.BaseAddress = new Uri("https://api.nbrb.by/"));
        
        builder.Services.AddTransient<IDbService, DbService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}