﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public interface IKonto5Service
    {
        List<KatKonto5> PobierzKonta5(SessionDetails sesja);
    }
}