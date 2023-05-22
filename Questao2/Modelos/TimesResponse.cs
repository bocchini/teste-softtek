using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;

namespace Questao2.Modelos;

public class TimeResponse
{

    [JsonProperty("page")]
    public long Page { get; set; }

    [JsonProperty("per_page")]
    public long PerPage { get; set; }

    [JsonProperty("total")]
    public long Total { get; set; }

    [JsonProperty("total_pages")]
    public long TotalPages { get; set; }

    [JsonProperty("data")]
    public IList<Datum> Data { get; set; }
}

public class Datum
{
    [JsonProperty("competition")]
    public string Competition { get; set; }

    [JsonProperty("year")]
    public long Year { get; set; }

    [JsonProperty("round")]
    public string Round { get; set; }

    [JsonProperty("team1")]
    public string Team1 { get; set; }

    [JsonProperty("team2")]
    public string Team2 { get; set; }

    [JsonProperty("team1goals")]
    public int Team1Goals { get; set; }

    [JsonProperty("team2goals")]
    public int Team2Goals { get; set; }
}

