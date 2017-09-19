using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
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

        public PracownicyService(PracownikDAO pracownikDao)
        {
            this._PracownikDao = pracownikDao;
        }

        public List<Pracownik> PobierzWszystkich()
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzPracownikow("*");

            return pracownicy;
        }

        public List<Pracownik> PobierzWszystkichZatrudnionych()
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzZatrudnionychPracownikow();

            return pracownicy;
        }

        public List<Pracownik> PobierzPozostalych()
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzPozostalychPracownikow();

            return pracownicy;
        }

        public Pracownik PobierzPoId(string numeread)
        {
            Pracownik pracownik = _PracownikDao.PobierzPracownikaPoId(numeread);

            return pracownik;
        }

        public List<Pracownik> ZnajdzPracownikow(string search)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPracownikow(search);

            return pracownicy;

        }
        public List<Pracownik> ZnajdzPracownikowPoTekscie(string search)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPracownikowPoTekscie(search);

            return pracownicy;
        }

        public List<Pracownik> ZnajdzZatrPracownikowPoTekscie(string search)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczZatrPracownikowPoTekscie(search);
            return pracownicy;
        }

        public List<Pracownik> ZnajdzPozostPracownikowPoTekscie(string search)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPozostZatrPracownikowPoTekscie(search);

            return pracownicy;
        }
    }
}
