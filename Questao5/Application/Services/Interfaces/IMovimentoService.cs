using Questao5.Application.Dtos;

namespace Questao5.Application.Services.Interfaces
{
    public interface IMovimentoService
    {
        Task<string> AdicionaMovimento(MovimentoDto movimentoDto);
    }
}
