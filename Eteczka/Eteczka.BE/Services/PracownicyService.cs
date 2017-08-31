using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using System.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
using Eteczka.BE.Mappers;

namespace Eteczka.BE.Services
{
    public class PracownicyService : IPracownicyService
    {
        private PracownikDAO _PracownikDao;
        private IMapowalnyDoPracownikDto _PracownikDtoMapper;

        public PracownicyService(PracownikDAO pracownikDao, IMapowalnyDoPracownikDto pracownikDtoMapper)
        {
            _PracownikDao = pracownikDao;
            this._PracownikDtoMapper = pracownikDtoMapper;
        }

        public List<PracownikDTO> PobierzWszystkich()
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzPracownikow();

            List<PracownikDTO> result = new List<PracownikDTO>();
            foreach (Pracownik pracownik in pracownicy)
            {
                PracownikDTO maciek = _PracownikDtoMapper.mapuj(pracownik);

                result.Add(maciek);

            }
            return result;
        }

        public PracownikDTO PobierzPoId(string numeread)
        {
            PracownikDTO pracownikDTO = null;
            Pracownik pracownik = _PracownikDao.PobierzPracownikaPoId(numeread);
            if (pracownik != null)
            {
                pracownikDTO = _PracownikDtoMapper.mapuj(pracownik);
            }
            
            return pracownikDTO;
        }


        public List<PracownikDTO>ZnajdzPracownikow(string search)
        {
            List<PracownikDTO> PracownicyDTO = new List<PracownikDTO>();
            List<Pracownik> Pracownicy = _PracownikDao.WyszukiwaczPracownikow(search);

            foreach (Pracownik pracownik in Pracownicy)
            {
                PracownikDTO worker = _PracownikDtoMapper.mapuj(pracownik);
                PracownicyDTO.Add(worker);


            }
            return PracownicyDTO;

        }
        public List<PracownikDTO> ZnajdzPracownikowPoTekcie(string search)
        {
            List<PracownikDTO> PracownicyDTO = new List<PracownikDTO>();
            List<Pracownik> Pracownicy = _PracownikDao.WyszukiwaczPracownikowPoTekscie(search);
            foreach (Pracownik pracownik in Pracownicy)
            {
                PracownikDTO pracownikDTO = _PracownikDtoMapper.mapuj(pracownik);
                PracownicyDTO.Add(pracownikDTO);

            }
            return PracownicyDTO;
        }
    }
}
