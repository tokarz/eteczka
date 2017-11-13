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
    public class RejonyDAO : IRejonyDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IRejonMapper _RejonMapper;
        private IConnection _Connection;

        public RejonyDAO(IDbConnectionFactory factory, IRejonMapper RejonMapper,  IConnection connection)
        {
            this._ConnectionFactory = factory;
            this._RejonMapper = RejonMapper;
            this._Connection = connection;
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

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczRejonyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatRejony\"; ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;

        }

        public List<KatRejony> PobieraczRejonow()
        {
            List<KatRejony> PobraneRejony = new List<KatRejony>();
            string sqlQuery = "SELECT * FROM \"KatRejony\" ORDER BY nazwa"; 

        IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
        DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                KatRejony pobranyRejon = _RejonMapper.MapujZSql(row);
                PobraneRejony.Add(pobranyRejon);
            }
            return PobraneRejony;
        }
        public List<KatRejony> PobieraczRejonowDlaFirmy (string firma)
        {
            List<KatRejony> PobraneRejony = new List<KatRejony>();
            string sqlQuery = "SELECT * FROM \"KatRejony\" where LOWER(firma) = '"+ (firma.ToLower().Trim()) +"' ORDER BY nazwa";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                KatRejony pobranyRejon = _RejonMapper.MapujZSql(row);
                PobraneRejony.Add(pobranyRejon);
            }
            return PobraneRejony;

        }

    }



}

