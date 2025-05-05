using System.Globalization;
using MauiLabs.Lab6.Entities;
using MauiLabs.Lab6.Services.Interfaces;

namespace MauiLabs.Lab6;

public partial class Lab6 : ContentPage
{
    private Rate[] _rates = [];

    private readonly IRateService _rateService;

    public Lab6(IRateService rateService)
    {
        InitializeComponent();

        _rateService = rateService;

        _ = UpdateRates();
    }

    private async Task UpdateRates()
    {
        _rates = (await _rateService.GetRates(DateTime.Now)).ToArray();

        var ratesAbbreviations = _rates.Select(rate => rate.Cur_Abbreviation).ToList();

        ratesAbbreviations.Add("BYN");

        PickerFrom.ItemsSource = ratesAbbreviations;
        PickerTo.ItemsSource = ratesAbbreviations;

        var rubRate = _rates.First(rate => rate.Cur_Abbreviation == "RUB");
        var eurRate = _rates.First(rate => rate.Cur_Abbreviation == "EUR");
        var usdRate = _rates.First(rate => rate.Cur_Abbreviation == "USD");
        var chfRate = _rates.First(rate => rate.Cur_Abbreviation == "CHF");
        var cnyRate = _rates.First(rate => rate.Cur_Abbreviation == "CNY");
        var gbpRate = _rates.First(rate => rate.Cur_Abbreviation == "GBP");

        RubRate.Text = $"{rubRate.Cur_OfficialRate} for {rubRate.Cur_Scale} RUB";
        EurRate.Text = $"{eurRate.Cur_OfficialRate} for {eurRate.Cur_Scale} EUR";
        UsdRate.Text = $"{usdRate.Cur_OfficialRate} for {usdRate.Cur_Scale} USD";
        ChfRate.Text = $"{chfRate.Cur_OfficialRate} for {chfRate.Cur_Scale} CHF";
        CnyRate.Text = $"{cnyRate.Cur_OfficialRate} for {cnyRate.Cur_Scale} CNY";
        GbpRate.Text = $"{gbpRate.Cur_OfficialRate} for {gbpRate.Cur_Scale} GBP";
    }

    private async void UpdateRatesButton_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            await UpdateRates();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private void ConvertButton_OnClicked(object? sender, EventArgs e)
    {
        if (!decimal.TryParse(ConvertArgument.Text, out var value))
        {
            ConvertResult.Text = "Invalid input";
            return;
        }

        var fromCurrency = PickerFrom.SelectedItem?.ToString();
        var toCurrency = PickerTo.SelectedItem?.ToString();

        if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
        {
            ConvertResult.Text = "Select currencies";
            return;
        }

        if (fromCurrency == toCurrency)
        {
            ConvertResult.Text = value.ToString("N2", CultureInfo.CurrentCulture);
            return;
        }

        try
        {
            decimal result = 0;

            if (fromCurrency == "BYN")
            {
                var targetRate = _rates.FirstOrDefault(r => r.Cur_Abbreviation == toCurrency);
                if (targetRate is null)
                {
                    ConvertResult.Text = "Rate not found";
                    return;
                }

                result = value * targetRate.Cur_Scale / targetRate.Cur_OfficialRate;
            }
            else if (toCurrency == "BYN")
            {
                var sourceRate = _rates.FirstOrDefault(r => r.Cur_Abbreviation == fromCurrency);
                if (sourceRate is null)
                {
                    ConvertResult.Text = "Rate not found";
                    return;
                }

                result = value * sourceRate.Cur_OfficialRate / sourceRate.Cur_Scale;
            }
            else
            {
                var sourceRate = _rates.FirstOrDefault(r => r.Cur_Abbreviation == fromCurrency);
                var targetRate = _rates.FirstOrDefault(r => r.Cur_Abbreviation == toCurrency);

                if (sourceRate is null || targetRate is null)
                {
                    ConvertResult.Text = "Rate not found";
                    return;
                }

                var bynAmount = value * sourceRate.Cur_OfficialRate / sourceRate.Cur_Scale;
                result = bynAmount * targetRate.Cur_Scale / targetRate.Cur_OfficialRate;
            }

            ConvertResult.Text = result.ToString("N2", CultureInfo.CurrentCulture);
        }
        catch (Exception ex)
        {
            ConvertResult.Text = "Error in conversion";
            Console.WriteLine(ex);
        }
    }
}