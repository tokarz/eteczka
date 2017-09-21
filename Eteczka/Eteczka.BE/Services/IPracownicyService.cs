using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public interface IPracownicyService
    {
        List<Pracownik> PobierzWszystkich(SessionDetails sessionDetails);
        List<Pracownik> PobierzWszystkichZatrudnionych();
        List<Pracownik> PobierzPozostalych();
        Pracownik PobierzPoId(string numeread);
        List<Pracownik> ZnajdzPracownikow(string search);
        List<Pracownik> ZnajdzPracownikowPoTekscie(string search);
        List<Pracownik> ZnajdzZatrPracownikowPoTekscie(string search);
        List<Pracownik> ZnajdzPozostPracownikowPoTekscie(string search);
    }
}
