using UI.Pages;

namespace UI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(ArtistsPageModel), typeof(ArtistsPageModel));
        Routing.RegisterRoute(nameof(ArtistCreationPageModel), typeof(ArtistCreationPageModel));
        
        Routing.RegisterRoute(nameof(SongDetailsPageModel), typeof(SongDetailsPageModel));
        Routing.RegisterRoute(nameof(SongCreationPageModel), typeof(SongCreationPageModel));
        Routing.RegisterRoute(nameof(SongUpdatePageModel), typeof(SongUpdatePageModel));
    }
}