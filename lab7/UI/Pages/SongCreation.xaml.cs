using UI.ViewModels;

namespace UI.Pages;

public partial class SongCreationPageModel : ContentPage
{
    public SongCreationPageModel(SongCreationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}