using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositorio.Interfaces
{
    public interface ISaldoContaCorrenteRepositorio
    {
        Task<IList<Saldo>> SaldoAsync(string idConta);
    }
}
