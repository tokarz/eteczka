using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;




namespace Eteczka.BE.Utils
{
    public class PracownikUtils : WyszukiwaczPlikow
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

        public List<PracownikDTO> ZnajdzPracownikowZPlikiem(string plik, List<Pracownik> pracownicy)
        {
            List<PracownikDTO> PracownicyZPlikiem = new List<PracownikDTO>();
            List<PracownikDTO> PracownicyDTO = new List<PracownikDTO>();


            foreach (Pracownik pracownik in pracownicy)
            {
                PracownikDTO pracownikDTO = new PracownikDTO();


                pracownikDTO.Pliki = pracownik.Pliki;
                pracownikDTO.Id = pracownik.Id;
                pracownikDTO.Imie = pracownik.Imie;
                pracownikDTO.Nazwisko = pracownik.Nazwisko;
                pracownikDTO.PESEL = pracownik.PESEL;
                pracownikDTO.DataUrodzenia = pracownik.DataUrodzenia;
                pracownikDTO.NumerPracownika = pracownik.NumerPracownika;

                PracownicyDTO.Add(pracownikDTO);
            }
            foreach (PracownikDTO workerDTO in PracownicyDTO)
            {

                foreach (string sciezka in workerDTO.Pliki)
                    if (sciezka.Contains(plik))
                        PracownicyZPlikiem.Add(workerDTO);
            }
            return PracownicyZPlikiem;
        }
    }
}