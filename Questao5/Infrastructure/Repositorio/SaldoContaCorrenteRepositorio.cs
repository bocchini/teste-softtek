using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;
using Questao5.Infrastructure.Repositorio.Interfaces;

namespace Questao5.Infrastructure.Repositorio
{
    public class SaldoContaCorrenteRepositorio :RepositorioBase, ISaldoContaCorrenteRepositorio
    {
        private readonly string sql = "SELECT movimento.idmovimento, movimento.idcontacorrente, movimento.datamovimento, movimento.tipomovimento, movimento.valor, contacorrente.numero, contacorrente.nome, contacorrente.ativo FROM movimento INNER JOIN contacorrente ON movimento.idcontacorrente = contacorrente.idcontacorrente WHERE movimento.idcontacorrente = @idContaCorrente";
        public SaldoContaCorrenteRepositorio(DbSession db) : base(db)
        {
        }

        public async Task<IList<Saldo>> SaldoAsync(string idContaCorrente)
        {
            using (var conn = _db.Connection)
            {
                var saldo = await conn.QueryAsync<Saldo>(sql, new { idContaCorrente });


                return (IList<Saldo>)saldo;
            }
        }
    }
}
