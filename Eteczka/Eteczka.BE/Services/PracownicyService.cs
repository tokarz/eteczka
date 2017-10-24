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
using Eteczka.Model.DTO;

namespace Eteczka.BE.Services
{
    public class PracownicyService : IPracownicyService
    {
        private IPracownikDAO _PracownikDao;

        public PracownicyService(IPracownikDAO pracownikDao)
        {
            this._PracownikDao = pracownikDao;
        }

        public List<Pracownik> PobierzWszystkich(SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzPracownikow(sesja.AktywnaFirma, sesja.AktywnyUser.Confidential);

            return pracownicy;
        }

        public List<Pracownik> PobierzWszystkichZatrudnionych(SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzZatrudnionychPracownikow(sesja.AktywnaFirma, sesja.AktywnyUser.Confidential);

            return pracownicy;
        }

        public List<Pracownik> PobierzPozostalych(SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzPozostalychPracownikow(sesja.AktywnaFirma, sesja.AktywnyUser.Confidential);

            return pracownicy;
        }

        public Pracownik PobierzPoId(string numeread)
        {
            Pracownik pracownik = _PracownikDao.PobierzPracownikaPoId(numeread);

            return pracownik;
        }

        public List<Pracownik> ZnajdzPracownikow(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPracownikow(search, sesja.AktywnaFirma, sesja.AktywnyUser.Confidential);

            return pracownicy;

        }
        public List<Pracownik> ZnajdzPracownikowPoTekscie(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPracownikowPoTekscie(search, sesja.AktywnaFirma, sesja.AktywnyUser.Confidential);

            return pracownicy;
        }

        public List<Pracownik> ZnajdzZatrPracownikowPoTekscie(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczZatrPracownikowPoTekscie(search, sesja.AktywnaFirma, sesja.AktywnyUser.Confidential);
            return pracownicy;
        }

        public List<Pracownik> ZnajdzPozostPracownikowPoTekscie(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPozostZatrPracownikowPoTekscie(search, sesja.AktywnaFirma, sesja.AktywnyUser.Confidential);

            return pracownicy;
        }

        public InsertResult DodajPracownika(Pracownik pracownik, SessionDetails sesja)
        {
            InsertResult result = new InsertResult();
            pracownik.Numeread = pracownik.Nazwisko.Substring(0, 3) + pracownik.Imie.Substring(0, 3) + pracownik.PESEL;
            Pracownik pracownikWBazie = _PracownikDao.PobierzPracownikaPoId(pracownik.Numeread);
            if(pracownikWBazie != null)
            {
                result.Result = false;
                result.Message = "Pracownik o tym identyfikatorze juz widnieje w bazie! Sprawdz Pesel, Imie i Nazwisko";
            }
            else
            {
                result.Result = _PracownikDao.DodajPracownika(pracownik, sesja.AktywnyUser.Identyfikator, sesja.AktywnyUser.Identyfikator);
            }


            return result;
        }

        public InsertResult EdytujPracownika(Pracownik pracownik, SessionDetails sesja)
        {
            InsertResult result = new InsertResult();
            Pracownik pracownikWBazie = _PracownikDao.PobierzPracownikaPoId(pracownik.Numeread);
            if (pracownikWBazie == null)
            {
                result.Result = false;
                result.Message = "Pracownik o tym identyfikatorze nie widnieje w bazie! Sprawdz Pesel, Imie i Nazwisko";
            }
            else
            {
                result.Result = _PracownikDao.EdytujPracownika(pracownik, sesja.AktywnyUser.Identyfikator, sesja.AktywnyUser.Identyfikator);
            }


            return result;
        }
    }
}
