using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;




namespace Eteczka.BE.Utils
{
    public class PracownikUtils
    {
        public List<PracownikDTO> ZnajdzPracownikowZPlikiem(string plik, List<PracownikDTO> pracownicy)
        {
            List<PracownikDTO> PracownicyZPlikiem = new List<PracownikDTO>();

            foreach (PracownikDTO pracownik in pracownicy)
            {
                foreach (string sciezka in pracownik.Pliki)
                {
                    if (sciezka.Contains(plik))
                    {
                        PracownicyZPlikiem.Add(pracownik);
                    }
                }

            }

            return PracownicyZPlikiem;
        }
    }
}