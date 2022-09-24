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
    private readonly IConnectivity _connectivity;
    private readonly IGeolocation _geolocation;

    public MonkeyViewModel(MonkeyService monkeyService, 
                           IConnectivity connectivity, 
                           IGeolocation geolocation)
    {
        Title = "Monkey Finder";
        _monkeyService = monkeyService;
        Monkeys = new ObservableCollection<Monkey>();

        _connectivity = connectivity;
        _geolocation = geolocation;
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
            if(_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet Issue!",
                                                 $"Check your internet and try again",
                                                 "OK");
                return;
            }

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

    [RelayCommand]
    async Task GetClosestMonkey()
    {
        if (IsBusy || Monkeys.Count == 0)
            return;

        try
        {
            // Get cached location, else get real location.
            var location = await _geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await _geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            // Find closest monkey to us
            var first = Monkeys.MinBy(m => location.CalculateDistance(
                                                                      new Location(m.Latitude, m.Longitude), DistanceUnits.Miles));

            await Shell.Current.DisplayAlert("", first.Name + " " +
                                                 first.Location, "OK");

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to query location: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}