using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;

namespace Eteczka.DB.Connection
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private IConnectionDetails _ConnectionDetails;

        public DbConnectionFactory(IConnectionDetails connectionDetails)
        {
            this._ConnectionDetails = connectionDetails;
        }

        public IConnectionState CreateConnectionToDB(IConnection connection)
        {
            IDbConnection dbConnection = new NpgsqlConnection(connection.GetConnectionString(_ConnectionDetails));

            IConnectionState result = new ConnectionState(dbConnection);

            return result;
        }
    }
}
