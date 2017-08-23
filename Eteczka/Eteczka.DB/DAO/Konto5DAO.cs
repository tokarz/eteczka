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
    public class Konto5DAO
    {
        private IDbConnectionFactory _ConnectionFactory;

        public Konto5DAO(IDbConnectionFactory factory)
        {
            this._ConnectionFactory = factory;
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

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczRejonyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatKonta5\"; ";
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
