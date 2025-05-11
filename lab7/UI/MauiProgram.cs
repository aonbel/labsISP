using System.Reflection;
using Application;
using Application.Data;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using Persistence.Data;

namespace UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        const string settingsStream = "UI.appsettings.json";

        var builder = MauiApp.CreateBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(settingsStream)!;

        builder.Configuration.AddJsonStream(stream);

        var connectionString = builder.Configuration.GetConnectionString("SqliteConnection")!;

        var dataDirectory = FileSystem.AppDataDirectory;

        connectionString = string.Format(connectionString, dataDirectory);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connectionString)
            .Options;

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services
            .AddApplication()
            .AddPersistense(options)
            .AddPages()
            .AddViewModels()
            .AddUI();
        
        DbInitializer
            .Initialize(builder.Services.BuildServiceProvider())
            .Wait();

        return builder.Build();
    }
}