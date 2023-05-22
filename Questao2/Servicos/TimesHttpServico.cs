using Newtonsoft.Json;
using Questao2.Modelos;

namespace Questao2.Servicos;

public class TimesHttpServico
{
    private static string URL = "https://jsonmock.hackerrank.com/api/football_matches";
    private HttpClient _httpClient;

    public TimesHttpServico()
    {
        _httpClient = new HttpClient();
    }

    public async Task<TimeResponse> Get(int year, string team, int page)
    {
        var response = _httpClient.GetAsync($"{URL}?year={year}&team1={team}&page={page}").Result;

        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(result))
            {
                return JsonConvert.DeserializeObject<TimeResponse>(result);                
            }
        }
        
        return new TimeResponse();
    }
}
