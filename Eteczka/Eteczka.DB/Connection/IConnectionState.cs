using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Eteczka.DB.Connection
{
    public interface IConnectionState
    {
        DataTable ExecuteQuery(string query);
    }
}
