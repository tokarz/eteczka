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

            

            return PracownicyZPlikiem;
        }

        public List<PracownikDTO> ZnajdzPracownikowZPlikiem(string plik, List<Pracownik> pracownicy)
        {
            List<PracownikDTO> PracownicyZPlikiem = new List<PracownikDTO>();
            List<PracownikDTO> PracownicyDTO = new List<PracownikDTO>();
            
            
            return PracownicyZPlikiem;
        }
    }
}