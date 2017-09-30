using System.Collections.Generic;
using System.Text;
using Eteczka.Model.Entities;
using System.Data;
using Eteczka.DB.Connection;
using Eteczka.DB.Mappers;
using System;
using NLog;



namespace Eteczka.DB.DAO
{
    public class PracownikDAO
    {
        Logger LOGGER = LogManager.GetLogger("PracownikDAO");

        private IDbConnectionFactory _ConnectionFactory;
        private IPracownikMapper _PracownikMapper;
        private IConnection _Connection;

        public PracownikDAO(IDbConnectionFactory factory, IPracownikMapper pracownikMapper, IConnection connection)
        {
            this._ConnectionFactory = factory;
            this._PracownikMapper = pracownikMapper;
            this._Connection = connection;
        }

        public bool ImportujPracownikow(List<Pracownik> pracownicy)
        {
            StringBuilder queries = new StringBuilder();
            foreach (Pracownik pracownik in pracownicy)
            {
                string values = "'" + pracownik.Imie + "', '" + pracownik.Nazwisko + "', '" + pracownik.PESEL + "', '" + pracownik.Numeread + "', '" + pracownik.Kraj + "', '" + pracownik.NazwiskoRodowe + "', '" + pracownik.ImieMatki + "', '" + pracownik.ImieOjca + "', '" + pracownik.PeselInny + "', '" + pracownik.IdOper + "', '" + pracownik.IdAkcept + "', '" + pracownik.DataModify + "', '" + pracownik.DataAkcept + "', '" + pracownik.DataUrodzenia + "', '" + pracownik.Imie2 + "', 'EAD', false";
                string query = "INSERT INTO \"KatPracownicy\" (imie, nazwisko, pesel, numeread, kraj, nazwiskorodowe, imiematki, imieojca, peselinny, idoper, idakcept, datamodify, dataakcept, dataurodzenia, imie2, systembazowy, usuniety) VALUES (" + values + ");";
                queries.Append(query);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            bool result = connectionState.ExecuteNonQuery(queries.ToString());

            return result;
        }

        public List<Pracownik> PobierzPozostalychPracownikow(string firma, string orderby = "nazwisko,imie", bool asc = true)
        {
            string orderDirection = asc ? " ASC " : " DESC ";

//            string sqlQuery = "select * from \"KatPracownicy\" where numeread not in (select numeread from \"MiejscePracy\" where   firma in ('" + firma + "') and '02.09.2017 00:00:00' between \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) and numeread in (select numeread from \"MiejscePracy\") ORDER BY " + orderby + orderDirection;
            string sqlQuery = "select * from \"KatPracownicy\" where numeread not in (select numeread from \"MiejscePracy\" where firma in ('" + firma.Trim() + "') and '" + DateTime.Now.ToString().Substring(0,10) + "' between \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) and numeread in (select numeread from \"MiejscePracy\" where firma in ('" + firma.Trim() + "')) ORDER BY " + orderby + orderDirection;

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

        public List<Pracownik> PobierzZatrudnionychPracownikow(string firma, string orderby = "nazwisko,imie", bool asc = true)
        {
            LOGGER.Info("POBIERANIE ZATRUDNIONYCH PRACOWNIKOW DLA " + firma);
            string orderDirection = asc ? " ASC " : " DESC ";

            string sqlQuery = "select * from \"KatPracownicy\" where numeread in (select numeread from \"MiejscePracy\" where  firma in ('" + firma.Trim() + "') and '" + DateTime.Now.ToString().Substring(0,10) + "' between \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) ORDER BY " + orderby + orderDirection;
            List<Pracownik> fetchedUsers = new List<Pracownik>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            int x = 0;
            foreach (DataRow row in result.Rows)
            {
                Pracownik fetchedUser = _PracownikMapper.MapujZSql(row);
                x += 1;
                fetchedUsers.Add(fetchedUser);
            }

            return fetchedUsers;
        }

        public List<Pracownik> PobierzPracownikow(string firma, string limit = "*", string offset = "0", string orderby = "nazwisko,imie", bool asc = true)
        {
            string orderDirection = asc ? " ASC " : " DESC ";

            string sqlQuery = "SELECT * from \"KatPracownicy\" where numeread in (select numeread from \"MiejscePracy\" where firma = '" + firma.Trim() + "') ORDER BY " + orderby + orderDirection + this.AddLimitOffsetStatement(limit, offset);
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

        public Pracownik PobierzPracownikaPoId(string numeread)
        {
            Pracownik pobranyPracownik = new Pracownik();
            string sqlQuery = "SELECT * FROM \"KatPracownicy\" WHERE LOWER (numeread) = '" + (numeread.ToLower().Trim()) + "' ";
            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                DataTable result = connectionState.ExecuteQuery(sqlQuery);

                if (result.Rows.Count == 1)
                {
                    pobranyPracownik = _PracownikMapper.MapujZSql(result.Rows[0]);
                }
            }

            catch (Exception ex)
            {
                LOGGER.Error("PobierzPracownikaPoId [" + numeread + "]", ex);
            }

            return pobranyPracownik;
        }

        public List<Pracownik> WyszukiwaczPracownikow(string search, string firma)
        {
            List<Pracownik> WyszukaniPracownicy = new List<Pracownik>();

            string sqlQuery =
                "SELECT * " +
                "FROM \"KatPracownicy\" " +
                "WHERE  pesel = '" + (search.ToLower().Trim()) + "'  AND " +
                "       not usuniety AND " +
                "       confidential < 10 AND " +
                "       numeread in (SELECT numeread FROM \"MiejscePracy\" WHERE firma IN ('" + firma.Trim() + "'));";

            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                DataTable result = connectionState.ExecuteQuery(sqlQuery);

                foreach (DataRow row in result.Rows)
                {
                    Pracownik fetchedPracownik = _PracownikMapper.MapujZSql(row);
                    WyszukaniPracownicy.Add(fetchedPracownik);
                }
            }
            catch (Exception ex)
            {
                LOGGER.Error("WyszukiwaczPracownikow [" + search + "]", ex);
            }

            return WyszukaniPracownicy;
        }

        public List<Pracownik> WyszukiwaczPracownikowPoTekscie(string search, string firma, int limit = 100, string orderby = "nazwisko", bool asc = true)
        {
            List<Pracownik> WyszukaniPracownicyPoTekscie = new List<Pracownik>();
            string orderDirection = asc ? " ASC " : " DESC ";


            //string sqlQuery = "SELECT * FROM \"KatPracownicy\" WHERE  LOWER (nazwisko) || ' ' || LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ORDER BY " + orderby + orderDirection + "LIMIT " + limit;

            //string sqlQuery = "SELECT * FROM \"KatPracownicy\" WHERE  numeread in (select numeread from \"MiejscePracy\" where  firma = " + firma + " and LOWER (nazwisko) || ' ' || LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ORDER BY " + orderby + orderDirection + "LIMIT " + limit;
            string sqlQuery = "SELECT * FROM \"KatPracownicy\" where LOWER (nazwisko) || ' ' || LOWER (imie) || ' ' || pesel LIKE '%" + (search.ToLower().Trim()) + "%'  and not usuniety and confidential < 8 and numeread in (select numeread from \"MiejscePracy\" where firma IN ('" + firma.Trim() + "')) ORDER BY nazwisko,imie;";

            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                DataTable result = connectionState.ExecuteQuery(sqlQuery);

                foreach (DataRow row in result.Rows)
                {
                    Pracownik fetchedPracownik = _PracownikMapper.MapujZSql(row);
                    WyszukaniPracownicyPoTekscie.Add(fetchedPracownik);
                }
            }
            catch (Exception ex)
            {
                LOGGER.Error("WyszukiwaczPracownikowPoTekscie [" + search + "]", ex);
            }

            return WyszukaniPracownicyPoTekscie;
        }

        public int PoliczPracownikowWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatPracownicy\";";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }

        private string AddLimitOffsetStatement(string limit, string offset)
        {
            string result = "";
            if (limit == "*")
            {
                result = "";
            }
            else
            {
                result = "LIMIT " + limit + " OFFSET " + offset;
            }

            return result;
        }

        public List<Pracownik> WyszukiwaczZatrPracownikowPoTekscie(string search, string firma, int limit = 500, string orderby = "nazwisko", bool asc = true)
        {
            List<Pracownik> WyszukaniPracownicyPoTekscie = new List<Pracownik>();
            string orderDirection = asc ? " ASC " : " DESC ";


            //string sqlQuery = "SELECT * FROM \"KatPracownicy\" where numeread in (select numeread from \"MiejscePracy\" where firma IN ('" + firma + "') and '" + DateTime.Now.ToString() + "' between \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) AND  LOWER (nazwisko) || ' ' || LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ORDER BY " + orderby + orderDirection + "LIMIT " + limit;
            string sqlQuery = "SELECT * FROM \"KatPracownicy\" where LOWER(nazwisko) || ' ' || LOWER(imie) || ' ' || pesel LIKE '%" + (search.ToLower().Trim()) + "%' and not usuniety and confidential < 8 and numeread in (select numeread from \"MiejscePracy\" where firma IN ('" + firma.Trim() + "') and '" + DateTime.Now.ToString().Substring(0,10) + "'  between \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) ORDER BY nazwisko,imie; ";

            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                DataTable result = connectionState.ExecuteQuery(sqlQuery);

                foreach (DataRow row in result.Rows)
                {
                    Pracownik fetchedPracownik = _PracownikMapper.MapujZSql(row);
                    WyszukaniPracownicyPoTekscie.Add(fetchedPracownik);
                }
            }
            catch (Exception ex)
            {
                LOGGER.Error("WyszukiwaczZatrPracownikowPoTekscie [" + search + "]", ex);
            }

            return WyszukaniPracownicyPoTekscie;

        }

        public List<Pracownik> WyszukiwaczPozostZatrPracownikowPoTekscie(string search, string firma, int limit = 500, string orderby = "nazwisko", bool asc = true)
        {
            List<Pracownik> WyszukaniPracownicyPoTekscie = new List<Pracownik>();
            string orderDirection = asc ? " ASC " : " DESC ";

            //string sqlQuery = "SELECT * FROM \"KatPracownicy\" WHERE  LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (nazwisko) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ORDER BY " + orderby + orderDirection + "LIMIT " + limit;
            string sqlQuery = "SELECT * FROM \"KatPracownicy\" where numeread not in  (select numeread from \"MiejscePracy\" where firma IN ('" + firma.Trim() + "') and '" + DateTime.Now.ToString().Substring(0,10) + "' between \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) AND  LOWER (nazwisko) || ' ' || LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ORDER BY " + orderby + orderDirection + "LIMIT " + limit;

            try
            {
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                DataTable result = connectionState.ExecuteQuery(sqlQuery);

                foreach (DataRow row in result.Rows)
                {
                    Pracownik fetchedPracownik = _PracownikMapper.MapujZSql(row);
                    WyszukaniPracownicyPoTekscie.Add(fetchedPracownik);
                }
            }
            catch (Exception ex)
            {
                LOGGER.Error("WyszukiwaczPozostZatrPracownikowPoTekscie [" + search + "]", ex);
            }

            return WyszukaniPracownicyPoTekscie;

        }
    }
}
