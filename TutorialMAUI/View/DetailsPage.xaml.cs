using TutorialMAUI.Models;
using TutorialMAUI.ViewModel;

namespace TutorialMAUI.View;

[QueryProperty(nameof(Monkey), "Monkey")]
public partial class DetailsPage : ContentPage
{
    public DetailsPage(MonkeyDetailsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}