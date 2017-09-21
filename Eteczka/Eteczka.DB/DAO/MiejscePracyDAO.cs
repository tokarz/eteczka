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
            foreach (MiejscePracy miejscePracy in miejscaPracy)
            {
                string valuesLine = "('" + miejscePracy.Firma + "', '" + miejscePracy.Rejon + "','" + miejscePracy.Wydzial + "','" + miejscePracy.Podwydzial + "','" + miejscePracy.Konto5 + "','" + miejscePracy.DataPocz + "','" + miejscePracy.DataKoniec + "','" + miejscePracy.IdOper + "','" + miejscePracy.IdAkcept + "','" + miejscePracy.DataModify + "','" + miejscePracy.DataAkcept + "','" + miejscePracy.NumerEad + "', '" + miejscePracy.SystemBazowy + "', '" + miejscePracy.Usuniety + "', " + startId++ + ");";
                string singleImport = "INSERT INTO \"MiejscePracy\"(firma, rejon, wydzial, podwydzial, konto5, datapocz, datakoniec, idoper, idakcept, datamodify, dataakcept, numeread, systembazowy, usuniety, id) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public List<MiejscePracyDlaPracownika> PobierzMiejscaPracyDlaPracownika(string numerEad)
        {
            List<MiejscePracyDlaPracownika> result = new List<MiejscePracyDlaPracownika>();
            string sqlQuery = "SELECT datapocz, datakoniec, \"MiejscePracy\".firma, \"KatRejony\".nazwa as rejon, \"KatWydzial\".nazwa as wydzial, \"KatPodWydzial\".nazwa as podwydzial, konto5 from \"MiejscePracy\" left outer join \"KatRejony\" on \"MiejscePracy\".rejon = \"KatRejony\".rejon and \"MiejscePracy\".firma = \"KatRejony\".firma left outer join \"KatWydzial\" on \"MiejscePracy\".wydzial = \"KatWydzial\".wydzial and \"MiejscePracy\".firma = \"KatWydzial\".firma left outer join \"KatPodWydzial\" on \"MiejscePracy\".podwydzial = \"KatPodWydzial\".podwydzial and \"MiejscePracy\".firma = \"KatPodWydzial\".firma and \"MiejscePracy\".wydzial = \"KatPodWydzial\".wydzial where numeread = '" + numerEad + "';";
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
    }
}
