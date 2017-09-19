using Eteczka.DB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eteczka.DB.Connection
{
    public class Connection : IConnection
    {
        private CryptoUtils _Utils;
        public Connection(CryptoUtils utils)
        {
            this._Utils = utils;
        }

        public string GetConnectionString(IConnectionDetails connectionDetails)
        {
            return "User ID=" + connectionDetails.getUser() + ";Password=" + _Utils.CalculateMD5Hash(connectionDetails.getPassword()) + ";Server=" + connectionDetails.getHost() + ";Port=" + connectionDetails.getPort() + ";Database=" + connectionDetails.getDbName() + ";Pooling=true";
            //return "Driver={PostgreSQL UNICODE};Server=" + connectionDetails.getHost() + ";Port=" + connectionDetails.getPort() + ";Database=" + connectionDetails.getDbName() + "; Uid=" + connectionDetails.getUser() + ";Pwd=" + connectionDetails.getPassword();
        }
    }
}
