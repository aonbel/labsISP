using MauiLabs.Lab5.Entities.Core;
using SQLite;

namespace MauiLabs.Lab5.Entities;

[System.ComponentModel.DataAnnotations.Schema.Table("Exhibitors")]
public class Exhibitor : Entity
{
    [Indexed]
    public int HallId { get; set; }
}