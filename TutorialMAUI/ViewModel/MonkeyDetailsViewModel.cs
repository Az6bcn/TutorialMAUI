using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TutorialMAUI.Models;

namespace TutorialMAUI.ViewModel;

public partial class MonkeyDetailsViewModel: BaseViewModel
{
    public MonkeyDetailsViewModel()
    {
        
    }

    [ObservableProperty] 
    Monkey monkey;

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}