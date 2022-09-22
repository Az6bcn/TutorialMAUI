using System.Collections.ObjectModel;
using System.Diagnostics;
using TutorialMAUI.Models;
using TutorialMAUI.Services;
using CommunityToolkit.Mvvm.Input;
using TutorialMAUI.View;

namespace TutorialMAUI.ViewModel;

public partial class MonkeyViewModel: BaseViewModel
{
    private MonkeyService _monkeyService;
    private IConnectivity _connectivity;

    public MonkeyViewModel(MonkeyService monkeyService)
    {
        Title = "Monkey Finder";
        _monkeyService = monkeyService;
        Monkeys = new ObservableCollection<Monkey>();
    }

    public ObservableCollection<Monkey> Monkeys { get; set; }

    [RelayCommand]
    private async Task GoToDetailsAsync(Monkey monkey)
    {
        if (monkey is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}",
                                      animate: true,
                                      new Dictionary<string, object>() { { "Monkey", monkey } });
    }


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