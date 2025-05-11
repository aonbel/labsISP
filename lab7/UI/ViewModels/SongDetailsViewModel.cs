using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace UI.ViewModels;

[QueryProperty(nameof(SongViewModel), "SongViewModel")]
public partial class SongDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private SongViewModel _songViewModel;

    [RelayCommand]
    private async Task ReturnToArtistPage() => await ReturnToPreviousPage();

    private async Task ReturnToPreviousPage()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}