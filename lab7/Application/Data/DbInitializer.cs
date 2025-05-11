using Microsoft.Extensions.DependencyInjection;

namespace Application.Data;

public class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

        await unitOfWork.DeleteDatabase();
        await unitOfWork.CreateDatabase();

        // Adding Artists
        await unitOfWork.Artists.AddAsync(new Artist("Linkin Park", 29) { Id = 1 });
        await unitOfWork.Artists.AddAsync(new Artist("Imagine Dragons", 15) { Id = 2 });
        await unitOfWork.Artists.AddAsync(new Artist("Coldplay", 25) { Id = 3 });
        await unitOfWork.Artists.AddAsync(new Artist("Twenty One Pilots", 12) { Id = 4 });
        await unitOfWork.Artists.AddAsync(new Artist("The Killers", 20) { Id = 5 });
        await unitOfWork.Artists.AddAsync(new Artist("Muse", 27) { Id = 6 });
        await unitOfWork.Artists.AddAsync(new Artist("Foo Fighters", 28) { Id = 7 });
        await unitOfWork.Artists.AddAsync(new Artist("Paramore", 18) { Id = 8 });
        await unitOfWork.Artists.AddAsync(new Artist("Green Day", 31) { Id = 9 });
        await unitOfWork.Artists.AddAsync(new Artist("Panic! At The Disco", 16) { Id = 10 });

        await unitOfWork.SaveChanges();

        // Adding Songs with top rankings (1 to 30)
        // Linkin Park
        await unitOfWork.Songs.AddAsync(new Song("In the End", 
            new SongData(TimeSpan.FromMinutes(3)), 2, 1));
        await unitOfWork.Songs.AddAsync(new Song("Numb", 
            new SongData(TimeSpan.FromMinutes(3)), 5, 1));
        await unitOfWork.Songs.AddAsync(new Song("Crawling", 
            new SongData(TimeSpan.FromMinutes(3)), 12, 1));

        // Imagine Dragons
        await unitOfWork.Songs.AddAsync(new Song("Radioactive", 
            new SongData(TimeSpan.FromMinutes(3)), 7, 2));
        await unitOfWork.Songs.AddAsync(new Song("Believer", 
            new SongData(TimeSpan.FromMinutes(3)), 14, 2));
        await unitOfWork.Songs.AddAsync(new Song("Thunder", 
            new SongData(TimeSpan.FromMinutes(3)), 21, 2));

        // Coldplay
        await unitOfWork.Songs.AddAsync(new Song("Viva La Vida", 
            new SongData(TimeSpan.FromMinutes(4)), 1, 3));
        await unitOfWork.Songs.AddAsync(new Song("Yellow", 
            new SongData(TimeSpan.FromMinutes(4)), 6, 3));
        await unitOfWork.Songs.AddAsync(new Song("Clocks", 
            new SongData(TimeSpan.FromMinutes(4)), 13, 3));

        // Twenty One Pilots
        await unitOfWork.Songs.AddAsync(new Song("Stressed Out", 
            new SongData(TimeSpan.FromMinutes(3)), 8, 4));
        await unitOfWork.Songs.AddAsync(new Song("Heathens", 
            new SongData(TimeSpan.FromMinutes(3)), 15, 4));
        await unitOfWork.Songs.AddAsync(new Song("Ride", 
            new SongData(TimeSpan.FromMinutes(3)), 22, 4));

        // The Killers
        await unitOfWork.Songs.AddAsync(new Song("Mr. Brightside", 
            new SongData(TimeSpan.FromMinutes(3)), 3, 5));
        await unitOfWork.Songs.AddAsync(new Song("Somebody Told Me", 
            new SongData(TimeSpan.FromMinutes(3)), 10, 5));
        await unitOfWork.Songs.AddAsync(new Song("When You Were Young", 
            new SongData(TimeSpan.FromMinutes(3)), 17, 5));

        // Muse
        await unitOfWork.Songs.AddAsync(new Song("Hysteria", 
            new SongData(TimeSpan.FromMinutes(3)), 9, 6));
        await unitOfWork.Songs.AddAsync(new Song("Supermassive Black Hole", 
            new SongData(TimeSpan.FromMinutes(3)), 16, 6));
        await unitOfWork.Songs.AddAsync(new Song("Uprising", 
            new SongData(TimeSpan.FromMinutes(4)), 23, 6));

        // Foo Fighters
        await unitOfWork.Songs.AddAsync(new Song("Everlong", 
            new SongData(TimeSpan.FromMinutes(4)), 4, 7));
        await unitOfWork.Songs.AddAsync(new Song("Learn to Fly", 
            new SongData(TimeSpan.FromMinutes(4)), 11, 7));
        await unitOfWork.Songs.AddAsync(new Song("My Hero", 
            new SongData(TimeSpan.FromMinutes(4)), 18, 7));

        // Paramore
        await unitOfWork.Songs.AddAsync(new Song("Misery Business", 
            new SongData(TimeSpan.FromMinutes(3)), 19, 8));
        await unitOfWork.Songs.AddAsync(new Song("Ain't It Fun", 
            new SongData(TimeSpan.FromMinutes(3)), 25, 8));
        await unitOfWork.Songs.AddAsync(new Song("Still Into You", 
            new SongData(TimeSpan.FromMinutes(3)), 28, 8));

        // Green Day
        await unitOfWork.Songs.AddAsync(new Song("American Idiot", 
            new SongData(TimeSpan.FromMinutes(3)), 20, 9));
        await unitOfWork.Songs.AddAsync(new Song("Boulevard of Broken Dreams", 
            new SongData(TimeSpan.FromMinutes(4)), 24, 9));
        await unitOfWork.Songs.AddAsync(new Song("Basket Case", 
            new SongData(TimeSpan.FromMinutes(3)), 27, 9));

        // Panic! At The Disco
        await unitOfWork.Songs.AddAsync(new Song("I Write Sins Not Tragedies", 
            new SongData(TimeSpan.FromMinutes(3)), 26, 10));
        await unitOfWork.Songs.AddAsync(new Song("High Hopes", 
            new SongData(TimeSpan.FromMinutes(3)), 29, 10));
        await unitOfWork.Songs.AddAsync(new Song("Victorious", 
            new SongData(TimeSpan.FromMinutes(3)), 30, 10));

        await unitOfWork.SaveChanges();
    }
}