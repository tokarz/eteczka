using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eteczka.DB.Connection
{
    public class Connection : IConnection
    {
        public string GetConnectionString(IConnectionDetails connectionDetails)
        {
            return "User ID=postgres;Password=admin;Host=" + connectionDetails.getHost() + ";Port=" + connectionDetails.getPort() + ";Database=" + connectionDetails.getDbName() + ";Pooling=true";
        }
    }
}
