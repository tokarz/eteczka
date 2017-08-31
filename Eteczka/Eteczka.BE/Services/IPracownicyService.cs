using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;

namespace Eteczka.BE.Services
{
    public interface IPracownicyService
    {
        List<PracownikDTO> PobierzWszystkich();
        PracownikDTO PobierzPoId(string numeread);
        List<PracownikDTO> ZnajdzPracownikow(string search);
        List<PracownikDTO> ZnajdzPracownikowPoTekcie(string search);
    }
}
