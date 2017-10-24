﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Services
{
    public interface IPracownicyService
    {
        List<Pracownik> PobierzWszystkich(SessionDetails sessionDetails);
        List<Pracownik> PobierzWszystkichZatrudnionych(SessionDetails sesja);
        List<Pracownik> PobierzPozostalych(SessionDetails sesja);
        Pracownik PobierzPoId(string numeread);
        List<Pracownik> ZnajdzPracownikow(string search, SessionDetails sesja);
        List<Pracownik> ZnajdzPracownikowPoTekscie(string search, SessionDetails sesja);
        List<Pracownik> ZnajdzZatrPracownikowPoTekscie(string search, SessionDetails sesja);
        List<Pracownik> ZnajdzPozostPracownikowPoTekscie(string search, SessionDetails sesja);
        InsertResult DodajPracownika(Pracownik pracownik, SessionDetails sesja);
        InsertResult EdytujPracownika(Pracownik pracownik, SessionDetails sesja);

    }
}
