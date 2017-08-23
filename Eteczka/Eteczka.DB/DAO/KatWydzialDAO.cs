using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.DAO
{
    public class KatWydzialDAO
    {
        private IDbConnectionFactory _ConnectionFactory;

        public KatWydzialDAO(IDbConnectionFactory factory)
        {
            this._ConnectionFactory = factory;
        }

        public bool ImportujWydzialy(List<KatDzialy> dzialy)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (KatDzialy biezacyDzial in dzialy)
            {
                string valuesLine = "('" + biezacyDzial.Wydzial + "', '" + biezacyDzial.Nazwa + "','" + biezacyDzial.Datamodify + "','" + biezacyDzial.Idoper + "','" + biezacyDzial.Idakcept + "','" + biezacyDzial.Dataakcept + "','" + biezacyDzial.Firma + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatWydzial\"(wydzial, nazwa, datamodify, idoper, idakcept, dataakcept, firma, systembazowy, usuniety) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczWydzialyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatWydzial\"; ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }
    }
}
