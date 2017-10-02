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
    }
}
