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
    public class PracownikDAO : IPracownikDAO
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

        public List<Pracownik> PobierzPozostalychPracownikow(string firma, int confidential, string orderby = "nazwisko,imie", bool asc = true)
        {
            string dateShortFormat = "yyyy-MM-dd";
            string orderDirection = asc ? " ASC " : " DESC ";

            //            string sqlQuery = "select * from \"KatPracownicy\" where numeread not in (select numeread from \"MiejscePracy\" where   firma in ('" + firma + "') and '02.09.2017 00:00:00' between \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) and numeread in (select numeread from \"MiejscePracy\") ORDER BY " + orderby + orderDirection;
            string sqlQuery =
                "SELECT * FROM \"KatPracownicy\" "
                + " WHERE NOT usuniety AND confidential < " + confidential + " "
                + "AND numeread NOT IN "
                + "(select numeread FROM \"MiejscePracy\" WHERE firma IN "
                + "('" + firma.Trim() + "') AND '"
                + DateTime.Now.ToString(dateShortFormat)
                + "' BETWEEN \"MiejscePracy\".datapocz AND \"MiejscePracy\".datakoniec) "
                + "AND numeread IN "
                + "(SELECT numeread FROM \"MiejscePracy\" WHERE firma IN "
                + "('" + firma.Trim() + "')) ORDER BY " + orderby + orderDirection;

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

        public List<Pracownik> PobierzZatrudnionychPracownikow(string firma, int confidential, string orderby = "nazwisko,imie", bool asc = true)
        {
            LOGGER.Info("POBIERANIE ZATRUDNIONYCH PRACOWNIKOW DLA " + firma);
            string orderDirection = asc ? " ASC " : " DESC ";
            string dateShortFormat = "yyyy-MM-dd";

            //string sqlQuery = "select * from \"KatPracownicy\" where numeread in (select numeread from \"MiejscePracy\" where  firma in ('" + firma.Trim() + "') and '" + DateTime.Now.ToString(dateShortFormat) + "' between \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) ORDER BY " + orderby + orderDirection;
            string sqlQuery = "SELECT * FROM \"KatPracownicy\" "
                + "WHERE "
                + " NOT usuniety AND confidential < " + confidential + " "
                + "AND numeread IN "
                + "(SELECT numeread FROM \"MiejscePracy\" where "
                + "firma IN ('" + firma.Trim() + "') AND '"
                + DateTime.Now.ToString(dateShortFormat)
                + "' BETWEEN \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) "
                + "ORDER BY " + orderby + orderDirection;

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

        public List<Pracownik> PobierzPracownikow(string firma, int confidential, string limit = "*", string offset = "0", string orderby = "nazwisko,imie", bool asc = true)
        {
            string orderDirection = asc ? " ASC " : " DESC ";

            string sqlQuery =
                "SELECT * from \"KatPracownicy\" "
                + "WHERE "
                + "NOT usuniety AND "
                + "confidential < " + confidential + " "
                + "AND numeread IN "
                + "(SELECT numeread from \"MiejscePracy\" WHERE firma = '" + firma.Trim() + "') ORDER BY " + orderby + orderDirection + this.AddLimitOffsetStatement(limit, offset);
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
            Pracownik pobranyPracownik = null;
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

        public List<Pracownik> WyszukiwaczPracownikow(string search, string firma, int confidential)
        {
            List<Pracownik> WyszukaniPracownicy = new List<Pracownik>();

            //string sqlQuery =
            //    "SELECT * " +
            //    "FROM \"KatPracownicy\" " +
            //    "WHERE  pesel = '" + (search.ToLower().Trim()) + "'  AND " +
            //    "       not usuniety AND " +
            //    "       confidential < 10 AND " +
            //    "       numeread in (SELECT numeread FROM \"MiejscePracy\" WHERE firma IN ('" + firma.Trim() + "'));";

            string sqlQuery =
                "SELECT * "
                + "FROM \"KatPracownicy\" "
                + "WHERE "
                + "NOT usuniety AND "
                + "confidential < " + confidential + " "
                + "AND "
                + "pesel = '" + (search.ToLower().Trim()) + "'  AND " +
                "  numeread IN (SELECT numeread FROM \"MiejscePracy\" WHERE firma IN ('" + firma.Trim() + "'));";

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

        public List<Pracownik> WyszukiwaczPracownikowPoTekscie(string search, string firma, int confidential, int limit = 100, string orderby = "nazwisko", bool asc = true)
        {
            List<Pracownik> WyszukaniPracownicyPoTekscie = new List<Pracownik>();
            string orderDirection = asc ? " ASC " : " DESC ";


            //string sqlQuery = "SELECT * FROM \"KatPracownicy\" WHERE  LOWER (nazwisko) || ' ' || LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ORDER BY " + orderby + orderDirection + "LIMIT " + limit;

            //string sqlQuery = "SELECT * FROM \"KatPracownicy\" WHERE  numeread in (select numeread from \"MiejscePracy\" where  firma = " + firma + " and LOWER (nazwisko) || ' ' || LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ORDER BY " + orderby + orderDirection + "LIMIT " + limit;
            string sqlQuery =
                "SELECT * FROM \"KatPracownicy\" "
                + "WHERE "
                + "NOT usuniety AND confidential < " + confidential + " "
                + "AND LOWER (nazwisko) || ' ' || LOWER (imie) || ' ' || pesel LIKE "
                + "'%" + (search.ToLower().Trim()) + "%' "
                + " AND numeread IN (select numeread from \"MiejscePracy\" "
                + "WHERE firma IN ('" + firma.Trim() + "')) ORDER BY nazwisko,imie;";

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

        //Przekazać confidential do pracownikDAO i IpracownikDAO
        public List<Pracownik> WyszukiwaczZatrPracownikowPoTekscie(string search, string firma, int confidential, int limit = 500, string orderby = "nazwisko", bool asc = true)
        {
            List<Pracownik> WyszukaniPracownicyPoTekscie = new List<Pracownik>();
            string orderDirection = asc ? " ASC " : " DESC ";
            string dateShortFormat = "yyyy-MM-dd";


            //string sqlQuery = "SELECT * FROM \"KatPracownicy\" where numeread in (select numeread from \"MiejscePracy\" where firma IN ('" + firma + "') and '" + DateTime.Now.ToString() + "' between \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) AND  LOWER (nazwisko) || ' ' || LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ORDER BY " + orderby + orderDirection + "LIMIT " + limit;
            string sqlQuery =
                "SELECT " +
                    "*" +
                "FROM \"KatPracownicy\" " +
                "WHERE LOWER(nazwisko) || ' ' || LOWER(imie) || ' ' || pesel LIKE " +
                    "'%" + (search.ToLower().Trim()) + "%' " +
                    "AND NOT usuniety " +
                    "AND confidential < " + confidential + " " +
                    "AND numeread IN " +
                        "(SELECT numeread FROM \"MiejscePracy\" " +
                        "WHERE firma IN ('" + firma.Trim() + "') " +
                            "AND '" + DateTime.Now.ToString(dateShortFormat) +
                            "'  BETWEEN \"MiejscePracy\".datapocz AND \"MiejscePracy\".datakoniec) " +
                "ORDER BY nazwisko,imie; ";

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

        public List<Pracownik> WyszukiwaczPozostZatrPracownikowPoTekscie(string search, string firma, int confidential, int limit = 500, string orderby = "nazwisko", bool asc = true)
        {
            List<Pracownik> WyszukaniPracownicyPoTekscie = new List<Pracownik>();
            string orderDirection = asc ? " ASC " : " DESC ";
            string dateShortFormat = "yyyy-MM-dd";

            //            string sqlQuery = "SELECT * FROM \"KatPracownicy\" WHERE  LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (nazwisko) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ORDER BY " + orderby + orderDirection + "LIMIT " + limit;
            //string sqlQuery = "SELECT * FROM \"KatPracownicy\" where numeread not in  (select numeread from \"MiejscePracy\" where firma IN ('" + firma.Trim() + "') and '" + DateTime.Now.ToString().Substring(0,10) + "' between \"MiejscePracy\".datapocz and \"MiejscePracy\".datakoniec) AND  LOWER (nazwisko) || ' ' || LOWER (imie) LIKE '%" + (search.ToLower().Trim()) + "%' OR LOWER (pesel) LIKE '%" + (search.ToLower().Trim()) + "%' ORDER BY " + orderby + orderDirection + "LIMIT " + limit;

            string sqlQuery =
                "SELECT " +
                    "*" +
                "FROM \"KatPracownicy\" " +
                "WHERE LOWER(nazwisko) || ' ' || LOWER(imie) || ' ' || pesel LIKE " +
                    "'%" + (search.ToLower().Trim()) + "%' " +
                    "AND NOT usuniety " +
                    "AND confidential < " + confidential + " " +
                    "AND numeread NOT IN " +
                        "(SELECT numeread FROM \"MiejscePracy\" " +
                        "WHERE firma IN ('" + firma.Trim() + "') " +
                            "AND '" + DateTime.Now.ToString(dateShortFormat) +
                            "'  BETWEEN \"MiejscePracy\".datapocz AND \"MiejscePracy\".datakoniec) " +
                    "AND numeread IN " +
                        "(SELECT numeread FROM \"MiejscePracy\" " +
                        "WHERE firma IN ('" + firma.Trim() + "')) " +
                "ORDER BY nazwisko,imie; ";


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

        public bool DodajPracownika(Pracownik pracownik, string idoper, string idakcept)
        {
            bool success = false;
            try
            {
                object[] objects = new object[] {
                    pracownik.Imie,
                    pracownik.Nazwisko,
                    pracownik.PESEL,
                    pracownik.Numeread,
                    pracownik.Kraj,
                    pracownik.NazwiskoRodowe,
                    pracownik.ImieMatki,
                    pracownik.ImieOjca,
                    pracownik.PeselInny,
                    idoper,
                    idakcept,
                    pracownik.DataModify,
                    pracownik.DataAkcept,
                    pracownik.DataUrodzenia,
                    pracownik.Imie2,
                    "EAD",
                    false,
                    "",
                    0

                };
                string values = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', {16}, '{17}', {18}", objects);
                string sqlQuery = "INSERT INTO \"KatPracownicy\"(imie, nazwisko, pesel, numeread, kraj, nazwiskorodowe, imiematki, imieojca, peselinny, idoper, idakcept, datamodify, dataakcept, dataurodzenia, imie2, systembazowy, usuniety, kodkierownik, confidential) VALUES (" + values + ");";


                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                success = connectionState.ExecuteNonQuery(sqlQuery);

            }
            catch (Exception ex)
            {
                LOGGER.Error("Nieudane dodanie pracownika " + pracownik.ToString());
            }

            return success;
        }

        public bool DodajPracownikaZMiejscemPracy(Pracownik pracownikDoDodania, MiejscePracy miejsceDoDodania, string idoper, string idakcept)
        {
            bool sucess = false;

            try
            {
                object[] DanePracownika = new object[] {
                    pracownikDoDodania.Imie != null ? pracownikDoDodania.Imie.ToUpper() : pracownikDoDodania.Imie,
                    pracownikDoDodania.Nazwisko != null ? pracownikDoDodania.Nazwisko.ToUpper() : pracownikDoDodania.Nazwisko,
                    pracownikDoDodania.PESEL,
                    pracownikDoDodania.Numeread,
                    pracownikDoDodania.Kraj != null ? pracownikDoDodania.Kraj.ToUpper() : pracownikDoDodania.Kraj,
                    pracownikDoDodania.NazwiskoRodowe != null ? pracownikDoDodania.NazwiskoRodowe.ToUpper() : pracownikDoDodania.NazwiskoRodowe,
                    pracownikDoDodania.ImieMatki != null ? pracownikDoDodania.ImieMatki.ToUpper() : pracownikDoDodania.ImieMatki,
                    pracownikDoDodania.ImieOjca != null ? pracownikDoDodania.ImieOjca.ToUpper() : pracownikDoDodania.ImieOjca,
                    pracownikDoDodania.PeselInny,
                    idoper,
                    idakcept,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                    pracownikDoDodania.DataUrodzenia,
                     pracownikDoDodania.Imie2 != null ? pracownikDoDodania.Imie2.ToUpper() : pracownikDoDodania.Imie2,
                    "EAD",
                    false,
                    pracownikDoDodania.Kodkierownik,
                    pracownikDoDodania.Confidential

                };
                StringBuilder builder = new StringBuilder();
                string PracownikValues = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', {16}, '{17}', {18}", DanePracownika);
                string sqlQueryPracownik = "INSERT INTO \"KatPracownicy\"(imie, nazwisko, pesel, numeread, kraj, nazwiskorodowe, imiematki, imieojca, peselinny, idoper, idakcept, datamodify, dataakcept, dataurodzenia, imie2, systembazowy, usuniety, kodkierownik, confidential) VALUES (" + PracownikValues + ");";
                builder.Append(sqlQueryPracownik);

                object[] DaneMiejscaPracy = new object[]
                {
                miejsceDoDodania.Firma,
                miejsceDoDodania.Rejon,
                miejsceDoDodania.Wydzial,
                miejsceDoDodania.Podwydzial,
                miejsceDoDodania.Konto5,
                miejsceDoDodania.DataPocz.ToString("yyyy-MM-dd"),
                miejsceDoDodania.DataKoniec.ToString("yyyy-MM-dd"),
                idoper,
                idakcept,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                miejsceDoDodania.NumerEad,
                "EAD",
                false
                 };

                string MiejscePracyValues = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}'", DaneMiejscaPracy);
                string sqlQueryMiejscePracy = "INSERT INTO \"MiejscePracy\"(firma, rejon, wydzial, podwydzial, konto5, datapocz, datakoniec, idoper, idakcept,datamodify, dataakcept, numeread, systembazowy, usuniety) VALUES (" + MiejscePracyValues + ");";

                builder.Append(sqlQueryMiejscePracy);
                string sqlQuery = builder.ToString();

                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                sucess = connectionState.ExecuteNonQuery(sqlQuery);
            }
            catch (Exception ex)
            {

            }

            return sucess;

        }

        public bool EdytujPracownika(Pracownik pracownik, string idoper, string idakcept)
        {
            bool success = false;
            try
            {
                object[] objects = new object[] {
                    pracownik.Imie != null ? pracownik.Imie.ToUpper() : pracownik.Imie,
                    pracownik.Nazwisko != null ? pracownik.Nazwisko.ToUpper() : pracownik.Nazwisko,
                    pracownik.PESEL,
                    pracownik.Kraj != null ? pracownik.Kraj.ToUpper() : pracownik.Kraj,
                    pracownik.NazwiskoRodowe != null ? pracownik.NazwiskoRodowe.ToUpper() : pracownik.NazwiskoRodowe,
                    pracownik.ImieMatki != null ? pracownik.ImieMatki.ToUpper(): pracownik.ImieMatki,
                    pracownik.ImieOjca != null ? pracownik.ImieOjca.ToUpper() : pracownik.ImieOjca,
                    pracownik.PeselInny,
                    idoper,
                    idakcept,
                    pracownik.DataModify,
                    pracownik.DataAkcept,
                    pracownik.DataUrodzenia,
                    pracownik.Imie2 != null ? pracownik.Imie2.ToUpper() : pracownik.Imie2,
                    pracownik.SystemBazowy,
                    pracownik.Usuniety,
                    pracownik.Kodkierownik,
                    pracownik.Confidential
                };

                string updateQuery = string.Format("UPDATE \"KatPracownicy\" SET " +
                    "imie='{0}', nazwisko='{1}', pesel='{2}', kraj='{3}', nazwiskorodowe='{4}', " +
                   "imiematki='{5}', imieojca='{6}', peselinny='{7}', idoper='{8}', idakcept='{9}', datamodify='{10}'," +
                   "dataakcept='{11}', dataurodzenia='{12}', imie2='{13}', systembazowy='{14}', usuniety={15}, " +
                   "kodkierownik='{16}', confidential={17} WHERE numeread = '" + pracownik.Numeread + "';", objects);

                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                success = connectionState.ExecuteNonQuery(updateQuery);

            }
            catch (Exception ex)
            {
                LOGGER.Error("Nieudane dodanie pracownika " + pracownik.ToString());
            }

            return success;
        }
    }
}
