using Application.Features.Artists.Commands;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UI.Pages;

namespace UI.ViewModels;

public partial class ArtistCreationViewModel(ISender sender) : ObservableObject
{
    [ObservableProperty]
    private string _artistName = string.Empty;
    
    [ObservableProperty]
    private string _artistAge = string.Empty;
    
    [RelayCommand]
    private async Task Create() => await CreateArtist();

    private async Task CreateArtist()
    {
        if (string.IsNullOrEmpty(ArtistName))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter the name of the artist.", "OK");
            return;
        }
        
        if (!int.TryParse(ArtistAge, out var artistAge))
        {
            await Shell.Current.DisplayAlert("Error", "Age of the artist must be integer", "OK");
            return;
        }
        
        var command = new AddArtistCommand(ArtistName, artistAge);
        
        var response = await sender.Send(command);
        
        await Shell.Current.Navigation.PopAsync();
    }
}