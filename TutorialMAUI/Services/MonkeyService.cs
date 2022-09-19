using System.Net.Http.Json;
using TutorialMAUI.Models;

namespace TutorialMAUI.Services;

public class MonkeyService
{
    private HttpClient _httpClient;
    private ICollection<Monkey> _monkeyList;

    public MonkeyService()
    {
        _httpClient = new HttpClient();
        _monkeyList = new HashSet<Monkey>();
    }

    public async Task<ICollection<Monkey>> GetMonkeys()
    {
        if (_monkeyList.Any())
            return _monkeyList;

        var url = "https://montemagno.com/monkeys.json";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            _monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
            return _monkeyList;
        }

        return default;
    }
}