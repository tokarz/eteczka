﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.Model.Entities;

namespace Eteczka.BE.Utils
{
    public interface WyszukiwaczPlikow
    {
        List<Pracownik> ZnajdzPracownikowZPlikiem(string plik, List<Pracownik> pracownicy);
    }
}
