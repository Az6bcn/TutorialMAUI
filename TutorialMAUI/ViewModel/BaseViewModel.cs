

using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TutorialMAUI.ViewModel;

//[INotifyPropertyChanged]
public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
        
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool IsBusy;

    [ObservableProperty]
    string title;

    public bool IsNotBusy;

}