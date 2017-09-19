using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
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
        private IKatPodWydzialMapper _KatPodWydzialMapper;

        public KatPodwydzialDAO(IDbConnectionFactory factory, IKatPodWydzialMapper KatPodWydzialMapper)
        {
            this._ConnectionFactory = factory;
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

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczPodwydzialyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatPodWydzial\"; ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }
        public List<KatPodWydzialy> PobierzPodWydzialy()
        {

            List<KatPodWydzialy> PobranePodWydzialy = new List<KatPodWydzialy>();
            string sqlQuery = "SELECT * FROM \"KatPodWydzial\" ORDER BY nazwa";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable result = connectionState.ExecuteQuery(sqlQuery);

            foreach (DataRow row in result.Rows)
            {
                KatPodWydzialy PobranyPodWydzial = _KatPodWydzialMapper.MapujZSql(row);
                PobranePodWydzialy.Add(PobranyPodWydzial);
            }
            return PobranePodWydzialy;
        }
    }
}
