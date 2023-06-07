using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;
using Questao5.Infrastructure.Repositorio.Interfaces;

namespace Questao5.Infrastructure.Repositorio
{
    public class ContaCorrenteRepositorio : IContaCorrenteRepositorio
    {
        private DbSession _db;
        private readonly string sql = "SELECT idcontacorrente, numero, nome, ativo FROM contacorrente WHERE idcontacorrente = @idContaCorrente";

        public ContaCorrenteRepositorio(DbSession db)
        {
            _db = db;
        }

        public async Task<ContaCorrente> VerificaSeExiteConta(string idContaCorrente)
        {
            using (var conn = _db.Connection)
            {             
               return await conn.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { idContaCorrente });
            }
            
        }
    }
}
