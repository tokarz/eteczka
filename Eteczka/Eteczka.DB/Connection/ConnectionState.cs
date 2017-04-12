using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Eteczka.DB.Connection
{
    public class ConnectionState : IConnectionState
    {
        private IDbConnection _Connection;
        public ConnectionState(IDbConnection connection)
        {
            this._Connection = connection;
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable table = new DataTable();

            using (var cmd = _Connection.CreateCommand())
            {
                cmd.CommandText = query;
                _Connection.Open();
                table.Load(cmd.ExecuteReader());
                _Connection.Close();
            }

            return table;

        }
    }
}
