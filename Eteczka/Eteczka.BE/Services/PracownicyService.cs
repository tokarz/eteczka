using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.Entities;
using Eteczka.DB.DAO;

namespace Eteczka.BE.Services
{
    public class PracownicyService : IPracownicyService
    {
        private UserDAO _Dao;

        public List<PracownikDTO> PobierzWszystkich()
        {
            _Dao = new UserDAO(new DB.Connection.DbConnectionFactory());

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
