﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Connection
{
    public interface IConnection
    {
        string GetConnectionString(IConnectionDetails connectionDetails);
    }
}