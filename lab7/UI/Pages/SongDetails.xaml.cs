using UI.ViewModels;

namespace UI.Pages;

public partial class SongDetailsPageModel : ContentPage
{
    public SongDetailsPageModel(SongDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}