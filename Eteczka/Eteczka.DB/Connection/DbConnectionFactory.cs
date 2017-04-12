using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Eteczka.DB.Connection
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private IDbConnection _DbConnection;
        private IDbTransaction _Transaction;

        public IConnectionState CreateConnectionToDB(IConnection connection)
        {
            _DbConnection = new NpgsqlConnection(connection.GetConnectionString());

            IConnectionState result = new ConnectionState(_DbConnection);

            return result;
        }
    }
}
