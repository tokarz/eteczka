﻿using System.Data;
using Eteczka.Model.Entities;
using Eteczka.DB.Connection;
using System.Collections.Generic;
using Eteczka.DB.Mappers;

namespace Eteczka.DB.DAO
{
    public class UserDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IPracownikMapper _PracownikMapper;
        private IConnection _Connection;

        public UserDAO(IDbConnectionFactory factory, IPracownikMapper pracownikMapper, IConnection connection)
        {
            this._ConnectionFactory = factory;
            this._PracownikMapper = pracownikMapper;
            this._Connection = connection;
        }

        public Pracownik GetUserByName(string name)
        {
            string sqlQuery = "SELECT * from KatPracownicy where imie = '" + name + "';";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            Pracownik pracownik = null;
            if (result.Rows.Count == 1)
            {
                pracownik = _PracownikMapper.MapujZSql(result.Rows[0]);
            }


            return pracownik;
        }

        public Pracownik GetUserByPesel(string pesel)
        {
            string sqlQuery = "SELECT * from \"KatPracownicy\" where pesel = '" + pesel + "';";
            Pracownik fetchedUser = null;

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                fetchedUser = _PracownikMapper.MapujZSql(row);
            }

            return fetchedUser;
        }

        public List<Pracownik> GetAllUsers()
        {
            string sqlQuery = "SELECT * from \"KatPracownicy\";";
            List<Pracownik> fetchedUsers = new List<Pracownik>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                Pracownik fetchedUser = _PracownikMapper.MapujZSql(row);

                fetchedUsers.Add(fetchedUser);
            }

            return fetchedUsers;
        }

    }

}
