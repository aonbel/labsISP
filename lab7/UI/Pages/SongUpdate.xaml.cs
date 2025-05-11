using UI.ViewModels;

namespace UI.Pages;

public partial class SongUpdatePageModel : ContentPage
{
    public SongUpdatePageModel(SongUpdateViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}