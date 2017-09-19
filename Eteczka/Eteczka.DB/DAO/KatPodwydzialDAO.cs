using Eteczka.DB.Connection;
using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.DAO
{
    public class KatPodwydzialDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IConnection _Connection;

        public KatPodwydzialDAO(IDbConnectionFactory factory, IConnection connection)
        {
            this._ConnectionFactory = factory;
            this._Connection = connection;
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
    }
}
