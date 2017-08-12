using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Connection
{
    public interface IConnectionDetails
    {
        string getUser();
        string getPassword();
        string getHost();
        string getPort();
        string getDbName();
    }
}
