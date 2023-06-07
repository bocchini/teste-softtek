using Microsoft.Data.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.Database
{
    public class DbSession :IDisposable
    {
        public IDbConnection Connection { get; }

        public DbSession(IConfiguration configuration)
        {
            Connection = new SqliteConnection(configuration.GetValue<string>("DatabaseName"));
            Connection.Open();
        }

        public void Dispose()
         => Connection?.Dispose();
    }
}
