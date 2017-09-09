﻿using Eteczka.BE.DTO;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using System.Configuration;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public class KatLoginyService : IKatLoginyService
    {
        private KatLoginDAO _Dao;

        public KatLoginyService(KatLoginDAO dao)
        {
            this._Dao = dao;
        }

        public KatLoginy GetUserByNameAndPassword(string username, string password)
        {
            KatLoginy queryResult = _Dao.WczytajPracownikaPoNazwieIHasle(username, password);

            return queryResult;
        }

        public KatLoginyDetale GetUserDetails(string identyfikator)
        {
            KatLoginyDetale queryResult = _Dao.WczytajDetaleDlaUzytkownika(identyfikator);

            return queryResult;
        }
    }
}
