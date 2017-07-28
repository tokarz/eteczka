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
using Eteczka.DB.DAO;

namespace Eteczka.BE.Services
{
    public class PracownicyService : IPracownicyService
    {
        private UserDAO _Dao;
        private PracownikDAO _PracownikDao;

        public PracownicyService(UserDAO dao, PracownikDAO pracownikDao)
        {
            _Dao = dao;
            _PracownikDao = pracownikDao;
        }

        public List<PracownikDTO> PobierzWszystkich()
        {
            List<User> usrs = _Dao.GetAllUsers();

            List<PracownikDTO> pracownicy = new List<PracownikDTO>();
            foreach (User usr in usrs)
            {
                PracownikDTO maciek = new PracownikDTO
                {
                    Id = usr.Id + "",
                    DataUrodzenia = usr.DataUrodzenia,
                    Dzial = usr.Dzial,
                    Imie = usr.Imie,
                    Nazwisko = usr.Nazwisko,
                    PESEL = usr.DataUrodzenia
                };

                pracownicy.Add(maciek);

            }
            return pracownicy;
        }

        public PracownikDTO Pobierz(string name)
        {
            User usr = _Dao.GetUserByName(name);

            PracownikDTO maciek = new PracownikDTO
            {
                Id = usr.Id,
                DataUrodzenia = usr.DataUrodzenia,
                Dzial = usr.Dzial,
                Imie = usr.Imie,
                Nazwisko = usr.Nazwisko,
                PESEL = usr.PESEL
            };

            return maciek;

        }

        public List<PracownikDTO> PobierzDlaSpolki(string spolkaId)
        {
            return null;
        }

        public bool ImportujJson(string sessionId)
        {
            bool result = false;
            string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
            List<Pracownik> pracownicy = new List<Pracownik>();

            string sciezkaDoPlikow = Path.Combine(eadRoot, "zet");
            string plikDoImportu = Path.Combine(sciezkaDoPlikow, "JsonKopAdministrator.json");

            if (File.Exists(plikDoImportu))
            {
                StringBuilder zetDb = new StringBuilder();
                using (StreamReader reader = new StreamReader(plikDoImportu))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        zetDb.Append(line);
                    }
                };

                var parsedJson = JObject.Parse(zetDb.ToString());
                var root = parsedJson["Pracownicy"];
                foreach (var pracownik in root)
                {
                    Pracownik wczytanyPracownik = new Pracownik();
                    wczytanyPracownik.Id = pracownik["id"].ToString();
                    wczytanyPracownik.NumerPracownika = pracownik["nrprac"].ToString();
                    wczytanyPracownik.Nazwisko = pracownik["nazwisko"].ToString();
                    wczytanyPracownik.Imie = pracownik["imie"].ToString();
                    wczytanyPracownik.PESEL = pracownik["pesel"].ToString();

                    pracownicy.Add(wczytanyPracownik);
                }

                result = _PracownikDao.ImportujPracownikow(pracownicy);
            }
            return result;
        }

        public PracownikDTO PobierzPoPeselu(string pesel)
        {
            User usr = _Dao.GetUserByPesel(pesel);

            PracownikDTO maciek = new PracownikDTO
            {
                Id = usr.Id,
                DataUrodzenia = usr.DataUrodzenia,
                Dzial = usr.Dzial,
                Imie = usr.Imie,
                Nazwisko = usr.Nazwisko,
                PESEL = usr.PESEL
            };

            return maciek;
        }
    }
}
