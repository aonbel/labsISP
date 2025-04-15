using MauiLabs.Lab5.Entities;
using MauiLabs.Lab5.Services.Interfaces;

namespace MauiLabs.Lab5;

public partial class Lab5 : ContentPage
{
    private readonly IDbService _dbService;
    
    public Lab5(IDbService dbService)
    {
        InitializeComponent();
        
        _dbService = dbService;
        
        _dbService.Init();

        Picker.ItemsSource = _dbService.GetAllHalls().ToList();
    }

    private void OnGroupSelected(object? sender, EventArgs e)
    {
        var selectedItem = (Hall)Picker.SelectedItem;
        
        if (selectedItem is null)
        {
            ItemsCollection.ItemsSource = new List<Exhibitor>();
            return;
        }

        ItemsCollection.ItemsSource = _dbService.GetExhibitorsByHallId(selectedItem.Id);
    }
}