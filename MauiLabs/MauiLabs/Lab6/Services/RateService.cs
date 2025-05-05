using System.Text.Json;
using MauiLabs.Lab6.Entities;
using MauiLabs.Lab6.Services.Interfaces;

namespace MauiLabs.Lab6.Services;

public class RateService(HttpClient httpClient) : IRateService
{
    public async Task<IEnumerable<Rate>> GetRates(DateTime date)
    {
        using var message = new HttpRequestMessage(HttpMethod.Get, $"exrates/rates?ondate={date:yyyy-MM-dd}&periodicity=0");
        
        var response = await httpClient.SendAsync(message);
        
        response.EnsureSuccessStatusCode();
        
        var rates = JsonSerializer.Deserialize<Rate[]>(await response.Content.ReadAsStringAsync());

        if (rates is null)
        {
            throw new NullReferenceException(nameof(rates));
        }
        
        return rates;
    }
}