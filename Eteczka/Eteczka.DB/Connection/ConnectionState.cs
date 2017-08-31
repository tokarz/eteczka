using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data.SqlClient;

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

            //using (var cmd = _Connection.CreateCommand())
            //{
            //    cmd.CommandText = query;
            //    _Connection.Open();

            //    table.Load(cmd.ExecuteReader());

            //    _Connection.Close();
            //}
            try
            {
                _Connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, (NpgsqlConnection)_Connection);
                NpgsqlDataReader dr = command.ExecuteReader();
                table.Load(dr);

            }
            catch (Exception ex)
            {
                //Log
            }
            finally
            {
                _Connection.Close();
            }


            return table;

        }

        public bool ExecuteNonQuery(string query)
        {
            bool result = false;

            using (var cmd = _Connection.CreateCommand())
            {
                cmd.CommandText = query;
                _Connection.Open();
                cmd.ExecuteNonQuery();
                _Connection.Close();
            }

            return result;
        }


    }
}
