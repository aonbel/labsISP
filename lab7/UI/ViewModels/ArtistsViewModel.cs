using System.Collections.ObjectModel;
using Application.Features.Artists.Queries;
using Application.Features.Songs.Queries;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UI.Infrastructure;
using UI.Pages;

namespace UI.ViewModels;

public partial class ArtistsViewModel(ISender sender, ImageHandler imageHandler) : ObservableObject
{
    public ObservableCollection<Artist> Artists { get; } = [];

    public ObservableCollection<SongViewModel> Songs { get; } = [];

    [ObservableProperty] private Artist? _selectedArtist;

    [RelayCommand]
    private async Task UpdateGroupList()
    {
        var command = new GetAllArtistsQuery();

        var response = await sender.Send(command);

        Artist? newSelectedArtist = null;

        if (SelectedArtist is not null)
        {
            newSelectedArtist = response.FirstOrDefault(artist => artist.Id == SelectedArtist.Id);
        }

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            Artists.Clear();

            foreach (var artist in response)
            {
                Artists.Add(artist);
            }

            if (newSelectedArtist is not null)
            {
                SelectedArtist = newSelectedArtist;
            }
        });
    }

    [RelayCommand]
    private async Task UpdateMembersList()
    {
        if (SelectedArtist is null)
        {
            return;
        }

        var command = new GetSongsByArtistIdQuery(SelectedArtist.Id);

        var response = await sender.Send(command);

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            Songs.Clear();
            foreach (var song in response)
            {
                Songs.Add(new SongViewModel(song));
            }
        });
    }

    [RelayCommand]
    private async Task ShowSongDetailsPage(SongViewModel songViewModel)
    {
        var parameters = new Dictionary<string, object>
        {
            { "SongViewModel", songViewModel },
        };

        await Shell.Current.GoToAsync(nameof(SongDetailsPageModel), parameters);
    }

    [RelayCommand]
    private async Task ShowArtistCreationPage()
    {
        await Shell.Current.GoToAsync(nameof(ArtistCreationPageModel));
    }

    [RelayCommand]
    private async Task ShowSongCreationPage()
    {
        if (SelectedArtist is null)
        {
            return;
        }

        var parameters = new Dictionary<string, object>
        {
            { "Artist", SelectedArtist },
        };

        await Shell.Current.GoToAsync(nameof(SongCreationPageModel), parameters);
    }  
    
    [RelayCommand]
    private async Task ShowSongUpdatePage(SongViewModel songViewModel)
    {
        var parameters = new Dictionary<string, object>
        {
            { "SongViewModel", songViewModel },
        };

        await Shell.Current.GoToAsync(nameof(SongUpdatePageModel), parameters);
    }

    [RelayCommand]
    private async Task UpdatePictureForSong(SongViewModel songViewModel)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select an image",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                await imageHandler.SaveImageToStorage(songViewModel.Id.ToString(), result);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to pick image: {ex.Message}", "OK");
        }
    }
}