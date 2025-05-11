using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Application.Features.Songs.Commands;
using UI.Pages;

namespace UI.ViewModels;

[QueryProperty(nameof(Artist), "Artist")]
public partial class SongCreationViewModel(ISender sender) : ObservableObject
{
    [ObservableProperty]
    private Artist _artist;

    [ObservableProperty]
    private string _songName;

    [ObservableProperty]
    private string _songRating;

    [ObservableProperty]
    private string _songLength;

    [RelayCommand]
    private async Task Create()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(SongName))
            {
                await Shell.Current.DisplayAlert("Error", "Song name is required.", "OK");
                return;
            }

            if (!int.TryParse(SongRating, out var rating) || rating < 0 || rating > 10)
            {
                await Shell.Current.DisplayAlert("Error", "Rating must be a number between 0 and 10.", "OK");
                return;
            }

            if (!TryParseTimeSpan(SongLength, out var length))
            {
                await Shell.Current.DisplayAlert("Error", "Length must be in MM:SS format.", "OK");
                return;
            }
            
            var command = new AddSongCommand(SongName, length, rating, Artist.Id);

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