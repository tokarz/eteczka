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
    public class KatPodwydzialDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IConnection _Connection;
        private IKatPodWydzialMapper _KatPodWydzialMapper;

        public KatPodwydzialDAO(IDbConnectionFactory factory, IKatPodWydzialMapper KatPodWydzialMapper, IConnection connection)
        {
            this._ConnectionFactory = factory;
            this._Connection = connection;
            this._KatPodWydzialMapper = KatPodWydzialMapper;
        }

        public bool ImportujPodwydzialy(List<KatPodWydzialy> podwydzialy)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (KatPodWydzialy biezacyPodwydzial in podwydzialy)
            {
                string valuesLine = "('" + biezacyPodwydzial.Podwydzial + "', '" + biezacyPodwydzial.Nazwa + "','" + biezacyPodwydzial.Wydzial + "','" + biezacyPodwydzial.Datamodify + "','" + biezacyPodwydzial.Idoper + "','" + biezacyPodwydzial.Idakcept + "','" + biezacyPodwydzial.Dataakcept + "','" + biezacyPodwydzial.Firma + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatPodWydzial\"(podwydzial, nazwa, wydzial, datamodify, idoper, idakcept, dataakcept, firma, systembazowy, usuniety) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczPodwydzialyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatPodWydzial\"; ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }
        public List<KatPodWydzialy> PobierzPodWydzialy(string firma, string wydzial)
        {

            List<KatPodWydzialy> PobranePodWydzialy = new List<KatPodWydzialy>();
            //string sqlQuery = "SELECT * FROM \"KatPodWydzial\" ORDER BY nazwa";
            string sqlQuery = "SELECT * from \"KatPodWydzial\" where \"KatPodWydzial\".firma IN ('" + firma + "') and \"KatPodWydzial\".wydzial IN ('" + wydzial + "') ORDER BY firma, wydzial";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);

            foreach (DataRow row in result.Rows)
            {
                KatPodWydzialy PobranyPodWydzial = _KatPodWydzialMapper.MapujZSql(row);
                PobranePodWydzialy.Add(PobranyPodWydzial);
            }
            return PobranePodWydzialy;
        }

        public bool SprawdzCzyPodWydzialIstnieje(string firma, string wydzial, string podwydzial)
        {
            bool result = false;

            string sqlQuery = "SELECT COUNT (*) FROM \"KatPodWydzial\" WHERE firma = '" + firma + "' AND wydzial = '" + wydzial + "' AND podwydzial = '" + podwydzial + "'";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);

            DataTable table = connectionState.ExecuteQuery(sqlQuery);

            int count = 0;

            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                count = int.Parse(table.Rows[0][0].ToString());
            }
            result = count > 0 ? true : false;

            return result;
        }

        public bool DodajPodWydzial(KatPodWydzialy wydzialDoDodania, string idoper, string idakcept)
        {
            bool result = false;

            object[] values = new object[]
            {
                wydzialDoDodania.Podwydzial,
                wydzialDoDodania.Nazwa,
                wydzialDoDodania.Wydzial,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                idoper,
                idakcept,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                wydzialDoDodania.Firma,
                "EAD",
                false
            };

            string sqlQuery = string.Format("INSERT INTO \"KatPodWydzial\" (podwydzial, nazwa, wydzial, datamodify, idoper, idakcept, dataakcept, firma, systembazowy, usuniety) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')", values);

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

        public bool EdytujPodWydzial(KatPodWydzialy podWydzialDoEdycji, string idoper, string idakcept)
        {
            bool result = false;

            string sqlQuery = $"UPDATE \"KatPodWydzial\" SET nazwa = '{podWydzialDoEdycji.Nazwa}', datamodify = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms")}', idoper = '{idoper}', idakcept = '{idakcept}' " +
                $"WHERE firma = '{podWydzialDoEdycji.Firma}' AND wydzial = '{podWydzialDoEdycji.Wydzial}' AND podwydzial = '{podWydzialDoEdycji.Podwydzial}'";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

        public bool UsunPodWydzial(KatPodWydzialy podWydzialDoUsuniecia, string idoper, string idakcept)
        {
            bool result = false;

            string sqlQuery = $"UPDATE \"KatPodWydzial\" SET usuniety = 'true', idoper = '{idoper}', idakcept = '{idakcept}', datamodify = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms")}' " +
                $"WHERE firma = '{podWydzialDoUsuniecia.Firma}' AND wydzial = '{podWydzialDoUsuniecia.Wydzial}' AND podwydzial = '{podWydzialDoUsuniecia.Podwydzial}'";

            IConnectionState connectionDetail = _ConnectionFactory.CreateConnectionToDB(_Connection);

            result = connectionDetail.ExecuteNonQuery(sqlQuery);

            return result;
        }
    }
}
