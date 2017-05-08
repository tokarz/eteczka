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

namespace Eteczka.BE.Services
{
    public class PracownicyService : IPracownicyService
    {
        private UserDAO _Dao;

        public List<PracownikDTO> PobierzWszystkich()
        {
            string user = ConfigurationManager.AppSettings["dbuser"];
            string password = ConfigurationManager.AppSettings["dbpassword"];
            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            IConnectionDetails connectionDetails = new ConnectionDetails(user, password, host, port, name);
            IDbConnectionFactory factory = new DbConnectionFactory(connectionDetails);

            _Dao = new UserDAO(factory);

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
    }
}
