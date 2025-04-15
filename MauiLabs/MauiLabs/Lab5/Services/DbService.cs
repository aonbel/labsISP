using LoremNET;
using MauiLabs.Lab5.Entities;
using MauiLabs.Lab5.Services.Interfaces;
using SQLite;

namespace MauiLabs.Lab5.Services;

public class DbService : IDbService
{
    private const string database_filename = "db";
    private const int halls_count = 15;
    private const int exhibitors_count = 80;

    private const SQLiteOpenFlags
        flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadWrite;

    private SQLiteConnection? _db;

    public ICollection<Hall> GetAllHalls()
    {
        if (_db is null)
        {
            throw new NullReferenceException("db is not initialized");
        }

        return _db.Table<Hall>().ToList();
    }

    public ICollection<Exhibitor> GetExhibitorsByHallId(int hallId)
    {
        if (_db is null)
        {
            throw new NullReferenceException("db is not initialized");
        }

        return _db.Table<Exhibitor>().Where(e => e.HallId == hallId).ToList();
    }

    public void Init()
    {
        if (_db is not null)
        {
            return;
        }

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, database_filename);

        var needToInit = !File.Exists(dbPath);

        _db = new SQLiteConnection(dbPath, flags);

        if (!needToInit)
        {
            return;
        }

        _db.CreateTable<Hall>();
        _db.CreateTable<Exhibitor>();

        for (var i = 0; i < halls_count; i++)
        {
            _db.Insert(new Hall
            {
                Name = Lorem.Words(1, 3),
            });
        }

        for (var i = 0; i < exhibitors_count; i++)
        {
            _db.Insert(new Exhibitor
            {
                Name = Lorem.Words(3, 5),
                HallId = Random.Shared.Next(1, halls_count + 1),
            });
        }
    }
}