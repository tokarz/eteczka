﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;

namespace Eteczka.BE.Services
{
    public interface IKatDokumentyRodzajService
    {
        List<KatDokumentyRodzaj> PobierzRodzDok();
    }
}