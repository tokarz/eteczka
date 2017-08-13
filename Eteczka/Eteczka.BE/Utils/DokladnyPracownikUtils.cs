using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;
using Eteczka.BE.Mappers;

namespace Eteczka.BE.Utils
{
    public class DokladnyPracownikUtils : WyszukiwaczPlikow
    {
        private IMapowalnyDoPracownikDto _PracownikDtoMaper;

        public DokladnyPracownikUtils(IMapowalnyDoPracownikDto pracownikDtoMaper)
        {
            this._PracownikDtoMaper = pracownikDtoMaper;
        }

        public List<PracownikDTO> ZnajdzPracownikowZPlikiem(string plik, List<Pracownik> pracownicy)
        {
            List<PracownikDTO> PracownicyZPlikiem = new List<PracownikDTO>();
            List<PracownikDTO> PracownicyDTO = new List<PracownikDTO>();

            foreach (Pracownik pracownik in pracownicy)
            {
                PracownikDTO pracownikDTO = _PracownikDtoMaper.mapuj(pracownik);

                PracownicyDTO.Add(pracownikDTO);
            }
            foreach (PracownikDTO workerDTO in PracownicyDTO)
            {
                
            }

            return PracownicyZPlikiem;
        }
    }
}
