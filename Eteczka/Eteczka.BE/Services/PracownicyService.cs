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
using Eteczka.DB.Mappers;

namespace Eteczka.BE.Services
{
    public class PracownicyService : IPracownicyService
    {
        private IPracownikDAO _PracownikDao;
        private IPracownikZMiejscemPracyMapper _mapper;
        private IMiejscePracyService _miejscePracyService;

        public PracownicyService(IPracownikDAO pracownikDao, IPracownikZMiejscemPracyMapper mapper, IMiejscePracyService miejscePracyService)
        {
            this._PracownikDao = pracownikDao;
            this._mapper = mapper;
            this._miejscePracyService = miejscePracyService;
        }

        public List<Pracownik> PobierzWszystkich(SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzPracownikow(sesja.AktywnaFirma.Firma, sesja.AktywnaFirma.Confidential);

            return pracownicy;
        }

        public List<Pracownik> PobierzWszystkichZatrudnionych(SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzZatrudnionychPracownikow(sesja.AktywnaFirma.Firma, sesja.AktywnaFirma.Confidential);

            return pracownicy;
        }

        public List<Pracownik> PobierzPozostalych(SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.PobierzPozostalychPracownikow(sesja.AktywnaFirma.Firma, sesja.AktywnaFirma.Confidential);

            return pracownicy;
        }

        public Pracownik PobierzPoId(string numeread)
        {
            Pracownik pracownik = _PracownikDao.PobierzPracownikaPoId(numeread);

            return pracownik;
        }

        public List<Pracownik> ZnajdzPracownikow(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPracownikow(search, sesja.AktywnaFirma.Firma, sesja.AktywnaFirma.Confidential);

            return pracownicy;

        }
        public List<Pracownik> ZnajdzPracownikowPoTekscie(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPracownikowPoTekscie(search, sesja.AktywnaFirma.Firma, sesja.AktywnaFirma.Confidential);

            return pracownicy;
        }

        public List<Pracownik> ZnajdzZatrPracownikowPoTekscie(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczZatrPracownikowPoTekscie(search, sesja.AktywnaFirma.Firma, sesja.AktywnaFirma.Confidential);
            return pracownicy;
        }

        public List<Pracownik> ZnajdzPozostPracownikowPoTekscie(string search, SessionDetails sesja)
        {
            List<Pracownik> pracownicy = _PracownikDao.WyszukiwaczPozostZatrPracownikowPoTekscie(search, sesja.AktywnaFirma.Firma, sesja.AktywnaFirma.Confidential);

            return pracownicy;
        }

        public InsertResult DodajPracownika(Pracownik pracownik, SessionDetails sesja)
        {
            InsertResult result = new InsertResult();
            pracownik.Numeread = this.StworzNumerEad(pracownik);
            Pracownik pracownikWBazie = _PracownikDao.PobierzPracownikaPoId(pracownik.Numeread);
            if(pracownikWBazie != null)
            {
                result.Result = false;
                result.Message = "Pracownik o tym identyfikatorze już widnieje w bazie! Sprawdź Pesel, imię i nazwisko";
            }
            else
            {
                result.Result = _PracownikDao.DodajPracownika(pracownik, sesja.AktywnaFirma.Identyfikator, sesja.AktywnaFirma.Identyfikator);
            }


            return result;
        }

        public InsertResult DodajPracownikaIMiejscePracy(SessionDetails sesja, PracownikZMiejscemPracy pracownikDoDodania)
        {
            InsertResult result = new InsertResult();
            Pracownik pracownik = _mapper.MapujDoPracownika(pracownikDoDodania);

            if (pracownik.PESEL != null)
            {
                pracownik.Numeread = this.StworzNumerEad(pracownik);
            }
            else
            {
                pracownik.Numeread = this.StworzZastepczyNumerEad(pracownik);
            }
            
            MiejscePracy miejscePracy = _mapper.MapujDoMiejscaPracy(pracownikDoDodania);
            miejscePracy.NumerEad = pracownik.Numeread;
            Pracownik pracownikWbazie = _PracownikDao.PobierzPracownikaPoId(pracownik.Numeread);

            if (pracownikWbazie != null)
            {
                result = _miejscePracyService.DodajMiejscePracy(sesja, miejscePracy);
            }
            else
            {
                result.Result = _PracownikDao.DodajPracownikaZMiejscemPracy(pracownik, miejscePracy, sesja.IdUzytkownika.Trim(), sesja.IdUzytkownika.Trim());
                result.Message = result.Result ? "Pracownik z miejscem pracy został dodany." : "Próba dodania pracownika i miejsca pracy nie powiodła się.";
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
                result.Message = "Pracownik o tym identyfikatorze już widnieje w bazie! Sprawdź Pesel, imię i nazwisko";
            }
            else
            {
                result.Result = _PracownikDao.EdytujPracownika(pracownik, sesja.AktywnaFirma.Identyfikator, sesja.AktywnaFirma.Identyfikator);
            }

            return result;
        }

        public string StworzNumerEad(Pracownik pracownik)
        {
            string nrEad = pracownik.Nazwisko.Substring(0, 3).ToUpper() + pracownik.Imie.Substring(0, 3).ToUpper() + pracownik.PESEL;

            return nrEad;
        }

        public string StworzZastepczyNumerEad(Pracownik pracownik)
        {
            string[] dataClean = pracownik.DataUrodzenia.Split('-');
            StringBuilder builder = new StringBuilder();
             
            foreach (string s in dataClean)
            {
                builder.Append(s);
            }
                
            string dataString = builder.ToString();
            Random random = new Random();
            int randomNumber = random.Next(100, 999);

            string ZastepczyNumerEad = pracownik.Nazwisko.Substring(0, 3).ToUpper() + pracownik.Imie.Substring(0, 3).ToUpper() + dataString + randomNumber;

            return ZastepczyNumerEad;
        }
    }
}
