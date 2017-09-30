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
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public class PracownicyService : IPracownicyService
    {
        private PracownikDAO _PracownikDao;

        public PracownicyService(PracownikDAO pracownikDao)
        {
            this._PracownikDao = pracownikDao;
        }

        public List<Pracownik> PobierzWszystkich(SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzPracownikow(sesja.AktywnaFirma);

            return pracownicy;
        }

        public List<Pracownik> PobierzWszystkichZatrudnionych(SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzZatrudnionychPracownikow(sesja.AktywnaFirma);

            return pracownicy;
        }

        public List<Pracownik> PobierzPozostalych(SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzPozostalychPracownikow(sesja.AktywnaFirma);

            return pracownicy;
        }

        public Pracownik PobierzPoId(string numeread)
        {
            Pracownik pracownik = _PracownikDao.PobierzPracownikaPoId(numeread);

            return pracownik;
        }

        public List<Pracownik> ZnajdzPracownikow(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPracownikow(search, sesja.AktywnaFirma);

            return pracownicy;

        }
        public List<Pracownik> ZnajdzPracownikowPoTekscie(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPracownikowPoTekscie(search, sesja.AktywnaFirma);

            return pracownicy;
        }

        public List<Pracownik> ZnajdzZatrPracownikowPoTekscie(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczZatrPracownikowPoTekscie(search, sesja.AktywnaFirma);
            return pracownicy;
        }

        public List<Pracownik> ZnajdzPozostPracownikowPoTekscie(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPozostZatrPracownikowPoTekscie(search, sesja.AktywnaFirma);

            return pracownicy;
        }
    }
}
