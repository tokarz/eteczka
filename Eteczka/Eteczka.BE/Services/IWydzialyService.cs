﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;

namespace Eteczka.BE.Services
{
    public interface IWydzialyService
    {
        List<KatWydzialy> PobierzWydzialyDlaFirmy(string firma);

    }
}