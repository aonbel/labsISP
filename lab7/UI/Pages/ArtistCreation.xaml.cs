using UI.ViewModels;

namespace UI.Pages;

public partial class ArtistCreationPageModel : ContentPage
{
    public ArtistCreationPageModel(ArtistCreationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}