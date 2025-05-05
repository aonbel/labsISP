using MauiLabs.Lab6.Entities;

namespace MauiLabs.Lab6.Services.Interfaces;

public interface IRateService
{
    Task<IEnumerable<Rate>> GetRates(DateTime date);
}