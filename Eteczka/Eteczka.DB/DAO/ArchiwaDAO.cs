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
    public class ArchiwaDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IConnection _Connection;

        public ArchiwaDAO(IDbConnectionFactory factory, IConnection connection)
        {
            this._ConnectionFactory = factory;
            this._Connection = connection;
        }

        public bool ImportujArchiwa(List<KatLokalPapier> archiwa)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (KatLokalPapier biezacyPlik in archiwa)
            {
                string valuesLine = "('" + biezacyPlik.Firma + "', '" + biezacyPlik.LokalPapier + "','" + biezacyPlik.Nazwa + "','" + biezacyPlik.Ulica + "','" + biezacyPlik.Numerdomu + "','" + biezacyPlik.Numerlokalu + "','" + biezacyPlik.Miasto + "','" + biezacyPlik.Kodpocztowy + "','" + biezacyPlik.Poczta + "','" + biezacyPlik.Idoper + "','" + biezacyPlik.Idakcept + "','" + biezacyPlik.Datamodify + "', '" + biezacyPlik.Dataakcept + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatLokalPapier\"(firma, lokalpapier, nazwa, ulica, numerdomu, numerlokalu, miasto, kodpocztowy, poczta, idoper, idakcept, datamodify, dataakcept, systembazowy, usuniety) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczArchiwaWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatLokalPapier\"; ";
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
