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
        private IMapowalnyDoPracownikDto _Mapper;

        public PracownikUtils(IMapowalnyDoPracownikDto mapper)
        {
            this._Mapper = mapper;
        }

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
            
            foreach (Pracownik worker in pracownicy)
            {
                foreach (string sciezka in worker.Pliki)
                {
                    if (sciezka.Contains(plik))
                    {
                        PracownikDTO workerDTO = this._Mapper.mapuj(worker);
                        PracownicyZPlikiem.Add(workerDTO);
                        break;
                    }
                }

            }
            return PracownicyZPlikiem;
        }
    }
}