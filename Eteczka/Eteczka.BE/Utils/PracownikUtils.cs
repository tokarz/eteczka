using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.Model.Entities;
using Eteczka.BE.Mappers;

namespace Eteczka.BE.Utils
{
    public class PracownikUtils : WyszukiwaczPlikow
    {
        public List<Pracownik> ZnajdzPracownikowZPlikiem(string plik, List<Pracownik> pracownicy)
        {
            List<Pracownik> PracownicyZPlikiem = new List<Pracownik>();

            return PracownicyZPlikiem;
        }
    }
}