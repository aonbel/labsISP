using System.Collections.ObjectModel;
using Application.Features.Artists.Queries;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Application.Features.Songs.Commands;

namespace UI.ViewModels;

[QueryProperty(nameof(SongViewModel), "SongViewModel")]
public partial class SongUpdateViewModel(ISender sender) : ObservableObject
{
    public ObservableCollection<Artist> Artists { get; } = [];

    [ObservableProperty]
    private Artist? _selectedArtist;
    
    [ObservableProperty]
    private SongViewModel _songViewModel;

    [ObservableProperty]
    private string _songName;

    [ObservableProperty]
    private string _songRating;

    [ObservableProperty]
    private string _songLength;

    [RelayCommand]
    private async Task UpdatePageContent()
    {
        try
        {
            var command = new GetAllArtistsQuery();

            var response = await sender.Send(command);

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Artists.Clear();

                foreach (var artist in response)
                {
                    Artists.Add(artist);
                }
                
                SongName = SongViewModel.Name;
                SongRating = SongViewModel.Rating.ToString();
                SongLength = SongViewModel.Data.Length.ToString(@"mm\:ss");
                
                SelectedArtist = Artists.FirstOrDefault(artist => artist.Id == SongViewModel.ArtistId)!;
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to load artists: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task Update()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(SongName))
            {
                await Shell.Current.DisplayAlert("Error", "Song name is required.", "OK");
                return;
            }

            if (!int.TryParse(SongRating, out var rating) || rating < 0)
            {
                await Shell.Current.DisplayAlert("Error", "Rating must be a number greater then 0.", "OK");
                return;
            }

            if (!TryParseTimeSpan(SongLength, out var length))
            {
                await Shell.Current.DisplayAlert("Error", "Length must be in MM:SS format.", "OK");
                return;
            }

            if (SelectedArtist is null)
            {
                await Shell.Current.DisplayAlert("Error", "Please select an artist first.", "OK");
                return;
            }
            
            var command = new UpdateSongCommand(SongViewModel.Id, SongName, length, rating, SelectedArtist.Id);

            await sender.Send(command);

            await Shell.Current.Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to create song: {ex.Message}", "OK");
        }
    }

    private bool TryParseTimeSpan(string input, out TimeSpan timeSpan)
    {
        timeSpan = TimeSpan.Zero;
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var parts = input.Split(':');
        if (parts.Length != 2 || !int.TryParse(parts[0], out var minutes) || !int.TryParse(parts[1], out var seconds))
            return false;

        try
        {
            timeSpan = new TimeSpan(0, minutes, seconds);
            return true;
        }
        catch
        {
            return false;
        }
    }
}