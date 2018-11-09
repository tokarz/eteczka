using Eteczka.DB.Connection;
using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Mappers;
using Eteczka.Model.DTO;

namespace Eteczka.DB.DAO
{
    public class MiejscePracyDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IConnection _Connection;
        private MiejscePracyMapper _MiejscePracyMapper;

        public MiejscePracyDAO(IDbConnectionFactory factory, IConnection connection, MiejscePracyMapper miejscePracyMapper)
        {
            this._ConnectionFactory = factory;
            this._Connection = connection;
            this._MiejscePracyMapper = miejscePracyMapper;
        }

        public bool ImportujMiejscaPracy(List<MiejscePracy> miejscaPracy)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();
            int startId = 0;
            string dateShortFormat = "yyyy-MM-dd";
            foreach (MiejscePracy miejscePracy in miejscaPracy)
            {
                string valuesLine = "('" + miejscePracy.Firma + "', '" + miejscePracy.Rejon + "','" + miejscePracy.Wydzial + "','" + miejscePracy.Podwydzial + "','" + miejscePracy.Konto5 + "','" + miejscePracy.DataPocz.ToString(dateShortFormat) + "','" + miejscePracy.DataKoniec.ToString(dateShortFormat) + "','" + miejscePracy.IdOper + "','" + miejscePracy.IdAkcept + "','" + miejscePracy.DataModify + "','" + miejscePracy.DataAkcept + "','" + miejscePracy.NumerEad + "', '" + miejscePracy.SystemBazowy + "', '" + miejscePracy.Usuniety + "', " + startId++ + ");";
                string singleImport = "INSERT INTO \"MiejscePracy\"(firma, rejon, wydzial, podwydzial, konto5, datapocz, datakoniec, idoper, idakcept, datamodify, dataakcept, numeread, systembazowy, usuniety, id) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public List<MiejscePracyDlaPracownika> PobierzMiejscaPracyDlaPracownika(string numerEad, string firma)
        {
            List<MiejscePracyDlaPracownika> result = new List<MiejscePracyDlaPracownika>();
            //            string sqlQuery = "SELECT datapocz, datakoniec, \"MiejscePracy\".firma, \"KatRejony\".nazwa as rejon, \"KatWydzial\".nazwa as wydzial, \"KatPodWydzial\".nazwa as podwydzial, konto5 from \"MiejscePracy\" left outer join \"KatRejony\" on \"MiejscePracy\".rejon = \"KatRejony\".rejon and \"MiejscePracy\".firma = \"KatRejony\".firma left outer join \"KatWydzial\" on \"MiejscePracy\".wydzial = \"KatWydzial\".wydzial and \"MiejscePracy\".firma = \"KatWydzial\".firma left outer join \"KatPodWydzial\" on \"MiejscePracy\".podwydzial = \"KatPodWydzial\".podwydzial and \"MiejscePracy\".firma = \"KatPodWydzial\".firma and \"MiejscePracy\".wydzial = \"KatPodWydzial\".wydzial where numeread = '" + numerEad + "';";
            string sqlQuery =
                "SELECT " +
                    "datapocz," +
                    "datakoniec, " +
                    "\"MiejscePracy\".firma, " +
                    "\"MiejscePracy\".rejon, " +
                    "\"MiejscePracy\".wydzial, " +
                    "\"MiejscePracy\".podwydzial, " +
                    "\"KatRejony\".nazwa as rejonnazwa, " +
                    "\"KatWydzial\".nazwa as wydzialnazwa, " +
                    "\"KatPodWydzial\".nazwa as podwydzialnazwa, " +
                    "konto5, " +
                    "id " +
                "FROM \"MiejscePracy\" " +
                "LEFT OUTER JOIN \"KatRejony\" " +
                    "ON \"MiejscePracy\".firma = \"KatRejony\".firma AND \"MiejscePracy\".rejon = \"KatRejony\".rejon " +
                "LEFT OUTER JOIN \"KatWydzial\" " +
                    "ON \"MiejscePracy\".firma = \"KatWydzial\".firma AND " +
                        "\"MiejscePracy\".wydzial = \"KatWydzial\".wydzial " +
                "LEFT OUTER JOIN \"KatPodWydzial\" " +
                    "ON \"MiejscePracy\".firma = \"KatPodWydzial\".firma AND " +
                        "\"MiejscePracy\".wydzial = \"KatPodWydzial\".wydzial AND " +
                        "\"MiejscePracy\".podwydzial = \"KatPodWydzial\".podwydzial " +
                "WHERE  NOT \"MiejscePracy\".usuniety AND " +
                        "numeread = '" + numerEad.Trim() + "' AND " +
                        "\"MiejscePracy\".firma IN ( '" + firma.Trim() + "' ) " +
                "ORDER BY datapocz;";


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable fetchedResult = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in fetchedResult.Rows)
            {
                MiejscePracyDlaPracownika miejscePracyDto = _MiejscePracyMapper.MapujZSqlDto(row);
                result.Add(miejscePracyDto);
            }

            return result;
        }

        public int PoliczMiejscaPracyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"MiejscePracy\"; ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }
        public bool DodajMiejscePracy(MiejscePracy miejsceDoDodania, string idoper, string idakcept)
        {

            bool result = false;
            string sqlQuery = "INSERT INTO \"MiejscePracy\" (firma, rejon, wydzial, podwydzial, konto5, datapocz, datakoniec, idoper, idakcept, datamodify, dataakcept, numeread, systembazowy, usuniety)" +
                "VALUES('" + miejsceDoDodania.Firma +"','"+ miejsceDoDodania.Rejon +"','" + miejsceDoDodania.Wydzial +"','" + miejsceDoDodania.Podwydzial + "','" + miejsceDoDodania.Konto5 +"','" + miejsceDoDodania.DataPocz.ToString("yyyy-MM-dd") + "','" + miejsceDoDodania.DataKoniec.ToString("yyyy-MM-dd") + "','" + idoper.Trim() + "','" + idakcept.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "','" + miejsceDoDodania.NumerEad + "','EAD'," + "FALSE)";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);
            return result;
        }

        public bool EdytujMiejscePracy(MiejscePracy miejsceDoEdycji, string idoper, string idakcept)
        {
            bool result = false;
            object[] wartosci = new object[]
            {
                miejsceDoEdycji.Firma,
                miejsceDoEdycji.Rejon,
                miejsceDoEdycji.Wydzial,
                miejsceDoEdycji.Podwydzial,
                miejsceDoEdycji.Konto5,
                idoper,
                idakcept,
                miejsceDoEdycji.DataPocz.ToString("yyyy-MM-dd"),
                miejsceDoEdycji.DataKoniec.ToString("yyyy-MM-dd"),
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                miejsceDoEdycji.Id

            };

            string sqlQuery = string.Format("UPDATE \"MiejscePracy\" " +
                "SET firma = '{0}', rejon = '{1}', wydzial = '{2}', podwydzial = '{3}', konto5 = '{4}', " +
                "idoper = '{5}', idakcept = '{6}', datapocz = '{7}', datakoniec = '{8}', datamodify = '{9}' " +
                "WHERE id = '{10}'", wartosci);

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

        public bool UsunMiejscePracy(long id, string idoper, string idakcept)
        {
            bool result = false;
            string sqlQuery = "UPDATE \"MiejscePracy\" " +
                "SET usuniety = 'true', idoper = '" + idoper + "', idakcept = '" + idakcept + "', datamodify = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "'" +
                "WHERE id = '" + id + "'";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;

        }

        public bool CzyPracownikMaAktualneMiejscePracy(MiejscePracy miejsceDoSprawdzenia)
        {
            bool result = false;
            string sqlQuery = "SELECT id FROM \"MiejscePracy\" " +
                "WHERE firma = '" + miejsceDoSprawdzenia.Firma +
                "' AND numeread = '" + miejsceDoSprawdzenia.NumerEad +
                "' AND '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  " +
                "BETWEEN datapocz AND datakoniec";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable table = connectionState.ExecuteQuery(sqlQuery);

            if (table != null & table.Rows != null & table.Rows.Count > 0 )
            {
                result = true;
            }
            return result;
        }

        public bool CzyPracownikPosiadaWiecejNizJednoMiejscePracyWFirmie(MiejscePracy miejsceDoSprawdzenia)
        {
            bool result = false;
            string sqlQuery = "SELECT id FROM \"MiejscePracy\" " +
                   "WHERE firma = '" + miejsceDoSprawdzenia.Firma +
                   "' AND numeread = '" + miejsceDoSprawdzenia.NumerEad + "'";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable table = connectionState.ExecuteQuery(sqlQuery);

            if (table != null && table.Rows != null && table.Rows.Count > 1)
            {
                result = true;
            }
            return result;
        }

        public bool CzyMiejscePracyIstnieje (long id)
        {
            bool result = false;
            string sqlQuery = "SELECT id from \"MiejscePracy\" WHERE id = '" + id + "'";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable table = connectionState.ExecuteQuery(sqlQuery);

            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
