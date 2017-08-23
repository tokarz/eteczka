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
    public class MiejscePracyDAO
    {
        private IDbConnectionFactory _ConnectionFactory;

        public MiejscePracyDAO(IDbConnectionFactory factory)
        {
            this._ConnectionFactory = factory;
        }

        public bool ImportujMiejscaPracy(List<MiejscePracy> miejscaPracy)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();
            int startId = 0;
            foreach (MiejscePracy miejscePracy in miejscaPracy)
            {
                string valuesLine = "('" + miejscePracy.Firma + "', '" + miejscePracy.Rejon + "','" + miejscePracy.Wydzial + "','" + miejscePracy.Podwydzial + "','" + miejscePracy.Konto5 + "','" + miejscePracy.DataPocz + "','" + miejscePracy.DataKoniec + "','" + miejscePracy.IdOper + "','" + miejscePracy.IdAkcept + "','" + miejscePracy.DataModify + "','" + miejscePracy.DataAkcept + "','" + miejscePracy.NumerEad + "', '" + miejscePracy.SystemBazowy + "', '" + miejscePracy.Usuniety + "', " + startId++ + ");";
                string singleImport = "INSERT INTO \"MiejscePracy\"(firma, rejon, wydzial, podwydzial, konto5, datapocz, datakoniec, idoper, idakcept, datamodify, dataakcept, numeread, systembazowy, usuniety, id) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczMiejscaPracyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"MiejscePracy\"; ";
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
