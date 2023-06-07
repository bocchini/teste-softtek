using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;
using Questao5.Infrastructure.Repositorio.Interfaces;

namespace Questao5.Infrastructure.Repositorio
{
    public class MovimentoRepositorio :IMovimentoRepositorio
    {
        private DbSession _db;

        public MovimentoRepositorio(DbSession db)
        {
            _db = db;
        }

        public async Task<string> CriaMovimentoAsync(Movimento movimento)
        {
            using (var conn = _db.Connection)
            {
                var novaMovimentacao = new Movimento(movimento.IdMovimento, movimento.IdContaCorrente,movimento.TipoMovimento, movimento.DataMovimento, movimento.Valor);

                var parametros = new DynamicParameters(novaMovimentacao);
                
                var movimentacao = await conn.ExecuteAsync("INSERT INTO MOVIMENTO (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@IdMovimento,@IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)", parametros);
                 return movimentacao == 1 ? movimento.IdMovimento : "Erro ao cadastrar uma movimentação";
            }
        }
    }
}
