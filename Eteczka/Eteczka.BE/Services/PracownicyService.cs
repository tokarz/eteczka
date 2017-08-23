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
        private UserDAO _Dao;
        private PracownikDAO _PracownikDao;
        private IMapowalnyDoPracownikDto _PracownikDtoMapper;

        public PracownicyService(UserDAO dao, PracownikDAO pracownikDao, IMapowalnyDoPracownikDto pracownikDtoMapper)
        {
            _Dao = dao;
            _PracownikDao = pracownikDao;
            this._PracownikDtoMapper = pracownikDtoMapper;
        }

        public List<PracownikDTO> PobierzWszystkich()
        {
            List<Pracownik> pracownicy = _Dao.GetAllUsers();

            List<PracownikDTO> result = new List<PracownikDTO>();
            foreach (Pracownik pracownik in pracownicy)
            {
                PracownikDTO maciek = _PracownikDtoMapper.mapuj(pracownik);

                result.Add(maciek);

            }
            return result;
        }

        public PracownikDTO Pobierz(string name)
        {
            Pracownik pracownik = _Dao.GetUserByName(name);

            PracownikDTO maciek = _PracownikDtoMapper.mapuj(pracownik);

            return maciek;

        }

        public List<PracownikDTO> PobierzDlaSpolki(string spolkaId)
        {
            return null;
        }

        

        public PracownikDTO PobierzPoPeselu(string pesel)
        {
            Pracownik pracownik = _Dao.GetUserByPesel(pesel);

            PracownikDTO maciek = _PracownikDtoMapper.mapuj(pracownik);

            return maciek;
        }
        public PracownikDTO PobierzPoId(string numeread)
        {

            Pracownik pracownik = _PracownikDao.PobierzPracownikaPoId(numeread);
            PracownikDTO pracownikDTO= _PracownikDtoMapper.mapuj(pracownik);
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
    }
}
