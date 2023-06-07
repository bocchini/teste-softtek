using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositorio.Interfaces
{
    public interface IMovimentoRepositorio
    {
        Task<string> CriaMovimentoAsync(Movimento movimento);
    }
}
