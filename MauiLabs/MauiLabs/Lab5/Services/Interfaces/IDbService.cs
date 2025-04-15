using MauiLabs.Lab5.Entities;

namespace MauiLabs.Lab5.Services.Interfaces;

public interface IDbService
{
    ICollection<Hall> GetAllHalls();
    ICollection<Exhibitor> GetExhibitorsByHallId(int hallId);
    void Init();
}