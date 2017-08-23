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
    public class RejonyDAO
    {
        private IDbConnectionFactory _ConnectionFactory;

        public RejonyDAO(IDbConnectionFactory factory)
        {
            this._ConnectionFactory = factory;
        }

        public bool ImportujRejony(List<KatRejony> rejony)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (KatRejony biezacyPlik in rejony)
            {
                string valuesLine = "('" + biezacyPlik.Rejon + "', '" + biezacyPlik.Nazwa + "','" + biezacyPlik.Idoper + "','" + biezacyPlik.Idakcept + "','" + biezacyPlik.Firma + "','" + biezacyPlik.Datamodify + "','" + biezacyPlik.Dataakcept + "','" + biezacyPlik.Mnemonik + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatRejony\"(rejon, nazwa, idoper, idakcept, firma, datamodify, dataakcept, mnemonik, systembazowy, usuniety) VALUES";

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
            string sqlQuery = "SELECT COUNT(*) FROM \"KatRejony\"; ";
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
