using SQLite;

namespace MauiLabs.Lab5.Entities.Core;

public class Entity
{
    [PrimaryKey, AutoIncrement, Indexed]
    public int Id { get; set; }
    public string Name { get; set; }
}