using Questao5.Application.Dtos;

namespace Questao5.Application.Services.Interfaces
{
    public interface ISaldoService
    {
        Task<SaldoDto> BuscaSaldo(string idContaCorrente);
    }
}
