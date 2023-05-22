using Questao2.Modelos;

namespace Questao2.Servicos;

public class CalcularGolsServico
{
    private int quantidadeDeGols = 0;
    private readonly TimesHttpServico _timeServico;
    private readonly int _year;
    public readonly string _team;
    private int page = 1;


    public CalcularGolsServico(int year, string team)
    {
        _timeServico = new TimesHttpServico();
        _year = year;
        _team = team;
    }

    public async Task<int> PegarQuantidadeDeGols()
    {
        var timeResponse = await ChamarServicoDeQuantidade(_year, _team, page);
        CalularQuantidadeGols(timeResponse);
        await PaginasAsync(timeResponse);
        return quantidadeDeGols;
    }

    private async Task PaginasAsync(TimeResponse timeResponse)
    {
        page++;
        while (page <= timeResponse.TotalPages)
        {
            var repoostaPaginas = await ChamarServicoDeQuantidade(_year, _team, page);
            CalularQuantidadeGols(repoostaPaginas);
            page++;
        }
    }

    private void CalularQuantidadeGols(TimeResponse resultado)
    {
        foreach (var item in resultado.Data)
        {
            SomaQuantidade(item.Team1Goals);
        }
    }

    private void SomaQuantidade(int quantidadeDeGolsFeitos) => quantidadeDeGols += quantidadeDeGolsFeitos;

    private async Task<TimeResponse> ChamarServicoDeQuantidade(int year, string team, int page)
    {
        return await _timeServico.Get(year, team, page);

    }

}
