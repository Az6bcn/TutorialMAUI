using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialMAUI.ViewModel;

namespace TutorialMAUI.View;

public partial class MainPage : ContentPage
{
    public MainPage(MonkeyViewModel viewModel)
    {

        InitializeComponent();

        // Set the context(data source) for this page
        BindingContext = viewModel;
    }
}