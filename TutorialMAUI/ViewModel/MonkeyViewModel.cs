using System.Collections.ObjectModel;
using System.Diagnostics;
using TutorialMAUI.Models;
using TutorialMAUI.Services;
using CommunityToolkit.Mvvm.Input;

namespace TutorialMAUI.ViewModel;

public partial class MonkeyViewModel: BaseViewModel
{
    private MonkeyService _monkeyService;

    public MonkeyViewModel(MonkeyService monkeyService)
    {
        Title = "Monkey Finder";
        _monkeyService = monkeyService;
        Monkeys = new ObservableCollection<Monkey>();
    }

    public ObservableCollection<Monkey> Monkeys { get; set; }

    [RelayCommand]
    private async Task GetMonkeysAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var monkeys = await _monkeyService.GetMonkeys();

            if(Monkeys!.Any())
                Monkeys.Clear();

            foreach (var monkey in monkeys)
                Monkeys.Add(monkey);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);

            await Shell.Current.DisplayAlert("Error!",
                                             $"Unable to get monkeys: {ex.Message}",
                                             "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}