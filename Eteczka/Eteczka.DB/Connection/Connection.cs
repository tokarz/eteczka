using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Connection
{
    public class Connection : IConnection
    {
        public string GetConnectionString()
        {
            return "User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=Eteczka;Pooling=true";
        }
    }
}
