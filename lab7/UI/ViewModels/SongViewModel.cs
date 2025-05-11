using CommunityToolkit.Mvvm.ComponentModel;
namespace UI.ViewModels;

public partial class SongViewModel : ObservableObject
{
    private readonly Song _song;

    public SongViewModel(Song song)
    {
        _song = song;
    }

    public int Id => _song.Id;
    public string Name => _song.Name;
    public SongData Data => _song.Data;
    public int Rating => _song.Rating;
    public int ArtistId => _song.ArtistId;
}