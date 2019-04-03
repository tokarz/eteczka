using Eteczka.DB.Connection;
using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Mappers;

namespace Eteczka.DB.DAO
{
    public class Konto5DAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IConnection _Connection;
        private IKatKonto5Mapper _KatKonto5Mapper;

        public Konto5DAO(IDbConnectionFactory factory, IConnection connection, IKatKonto5Mapper KatKonto5Mapper)
        {
            this._ConnectionFactory = factory;
            this._Connection = connection;
            this._KatKonto5Mapper = KatKonto5Mapper;
        }

        public bool ImportujKonta5(List<KatKonto5> konta)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (KatKonto5 biezaceKonto in konta)
            {
                string valuesLine = "('" + biezaceKonto.Konto5 + "', '" + biezaceKonto.Nazwa + "','" + biezaceKonto.Idoper + "','" + biezaceKonto.Idakcept + "','" + biezaceKonto.Firma + "','" + biezaceKonto.Kontoskr + "','" + biezaceKonto.Datamodify + "','" + biezaceKonto.Dataakcept + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatKonta5\"(konto5, nazwa, idoper, idakcept, firma, kontoskr, datamodify, dataakcept, systembazowy, usuniety) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczRejonyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatKonta5\"; ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }
        public List<KatKonto5> pobierajKonto5DlaFirmy(string firma)
        {

            List<KatKonto5> PobraneKonta5 = new List<KatKonto5>();

            string sqlQuery = "Select * FROM \"KatKonta5\" WHERE firma IN ('"+ firma + "') ORDER BY konto5";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                KatKonto5 pobraneKonto = _KatKonto5Mapper.MapujZSql(row);

                PobraneKonta5.Add(pobraneKonto);

            }
            return PobraneKonta5;

        }

        public bool DodajKonto5(KatKonto5 konto, string idoper, string idakcept)
        {
            bool result = false;

            object[] values = new object[]
            {
                konto.Konto5,
                konto.Nazwa,
                idoper,
                idakcept,
                konto.Firma,
                konto.Kontoskr,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                "EAD",
                false
            };

            string sqlQuery = string.Format("INSERT INTO \"KatKonta5\" (konto5, nazwa, idoper, idakcept, firma, kontoskr, datamodify, dataakcept, systembazowy, usuniety)" +
                "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')", values);

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);

            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

        public bool EdytujKonto5 (KatKonto5 konto, string idoper, string idakcept)
        {
            bool result = false;

            string sqlQuery = $"UPDATE \"KatKonta5\" SET nazwa = '{konto.Nazwa}', idoper = '{idoper}', idakcept = '{idakcept}', kontoskr = '{konto.Kontoskr}', datamodify ='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms")}', dataakcept = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms")}' " +
                $"WHERE firma = '{konto.Firma}' AND konto5 = '{konto.Konto5}' ";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);

            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;

        }

        public bool UsunKonto5(string firma, string konto5, string idoper, string idakcept)
        {
            bool result = false;
            string sqlQuery = "UPDATE \"KatKonta5\" SET usuniety = 'true', idoper = '" + idoper + "', idakcept = '" + idakcept + "' , datamodify = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "' " +
                "WHERE firma = '" + firma + "' AND konto5 = '" + konto5 + "'";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);

            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

        public List<KatKonto5> WyszukajKonto5 (string firma, string search)
        {
            List<KatKonto5> result = new List<KatKonto5>();

            string sqlQuery = "SELECT * FROM \"KatKonta5\" WHERE LOWER(firma) = LOWER('" + firma + "') " +
                "AND LOWER(nazwa) like LOWER('%" + search + "%') OR LOWER(konto5) like LOWER('%" + search + "%') OR LOWER(kontoskr) like LOWER('%" + search + "%')";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable table = connectionState.ExecuteQuery(sqlQuery);

            foreach (DataRow row in table.Rows)
            {
                result.Add(_KatKonto5Mapper.MapujZSql(row));
            }

            return result;

        }

        public bool SprawdzCzyKonto5IstniejeWFirmie(string firma, string konto5)
        {
            bool result = false;

            string sqlQuery = $"SELECT COUNT (*) FROM \"KatKonta5\" WHERE firma = '{firma}' AND konto5 = '{konto5}'";

            IConnectionState connection = _ConnectionFactory.CreateConnectionToDB(_Connection);

            int count = 0;
            DataTable table = connection.ExecuteQuery(sqlQuery);

            if(table != null && table.Rows != null && table.Rows.Count > 0 )
            {
                count = int.Parse(table.Rows[0][0].ToString());
            }

            result = count > 0 ? true : false;

            return result;
        }
    }
}
