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

            foreach (string file in  new PracownikDTO().Pliki)  //new PracownikDTO().Pliki???
            {

                string nazwaPliku = new PlikiUtils().WezNazwePlikuZeSciezki(file);
                if (nazwaPliku.Contains(plik))
                {
                    PracownicyZPlikiem.Add(new PracownikDTO());
                }

            }

            return PracownicyZPlikiem;
        }
    }
}