using Questao5.Infrastructure.Database;

namespace Questao5.Infrastructure.Repositorio
{
    public abstract class RepositorioBase
    {
        protected DbSession _db;

        public RepositorioBase(DbSession db)
        {
            _db = db;
        }
    }
}
