using Eteczka.BE.DTO;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using System.Configuration;
using System.Collections.Generic;
using Eteczka.Model.Entities;

namespace Eteczka.BE.Services
{
    public class KatLoginyService : IKatLoginyService
    {
        private KatLoginDAO _Dao;

        public KatLoginyService(KatLoginDAO dao)
        {
            this._Dao = dao;
        }

        public bool UsunFirmeUzytkownika(KatLoginy user, string firma)
        {
            return this._Dao.UsunFirmeUzytkownika(user, firma);
        }

        public KatLoginy GetUserByNameAndPassword(string username, string password)
        {
            KatLoginy queryResult = _Dao.WczytajPracownikaPoNazwieIHasle(username, password);

            return queryResult;
        }

        public List<KatLoginyDetale> GetUserDetails(string identyfikator)
        {
            List<KatLoginyDetale> queryResult = _Dao.WczytajDetaleDlaUzytkownika(identyfikator);

            return queryResult;
        }

        public List<KatLoginyDetale> GetAllUsersDetails()
        {
            List<KatLoginyDetale> queryResult = _Dao.WczytajWszystkieDetale();

            return queryResult;
        }
    }
}
