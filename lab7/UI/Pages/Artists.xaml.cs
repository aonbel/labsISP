using UI.ViewModels;

namespace UI.Pages;

public partial class ArtistsPageModel : ContentPage
{
    public ArtistsPageModel(ArtistsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}