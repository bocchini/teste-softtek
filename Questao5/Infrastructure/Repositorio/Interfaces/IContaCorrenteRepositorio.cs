using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositorio.Interfaces
{
    public interface IContaCorrenteRepositorio
    {
        Task<ContaCorrente> VerificaSeExiteConta(string idContaCorrente);
    }
}
