using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TutorialMAUI.ViewModel;

public class BaseViewModel: INotifyPropertyChanged
{
    private bool _isBusy;
    private string _title;

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if(_isBusy == value)
                return;

            _isBusy = value;
            OnPropertyChanged(nameof(IsBusy));
            OnPropertyChanged(nameof(IsNotBusy));
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            if (_title == value)
                return;

            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public bool IsNotBusy => !IsBusy;
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        // If the event is not null i.e anyone subscribed to the event => then invoke it
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}