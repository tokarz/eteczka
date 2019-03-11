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
        public bool SprawdzCzyRejonIstniejeWFirmie(string rejon, string firma)
        {
            bool result = false;
            int count = 0;

            string sqlQuery = "SELECT COUNT (*) FROM \"KatRejony\" WHERE LOWER (firma) = '" + firma.ToLower() + "' AND LOWER(rejon) = '" + rejon.ToLower() + "'";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable table = connectionState.ExecuteQuery(sqlQuery);

            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                count = int.Parse(table.Rows[0][0].ToString());
            }

            result = count > 0 ? true : false;

            return result;
        }

        public bool DodajRejonDlaFirmy(KatRejony rejonDoDodania, string idoper, string idakcept)
        {
            bool result = false;

            object[] values = new object[]
            {
                rejonDoDodania.Rejon,
                rejonDoDodania.Nazwa,
                idoper,
                idakcept,
                rejonDoDodania.Firma,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                rejonDoDodania.Mnemonik,
                "EAD",
                false
            };

            string sqlQuery = string.Format("INSERT INTO \"KatRejony\" (rejon, nazwa, idoper, idakcept, firma, datamodify, dataakcept, mnemonik, systembazowy, usuniety) " +
                "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", values);

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;

        }

        public bool EdytujRejonDlaFirmy(KatRejony rejonDoEdycji, string rejonPrzedZmiana, string idoper, string idakcept)
        {
            bool result = false;

            object[] values = new object[]
            {
                rejonDoEdycji.Rejon,
                rejonDoEdycji.Nazwa,
                idoper,
                idakcept,
                rejonDoEdycji.Firma,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                rejonDoEdycji.Mnemonik,
                "EAD",
                false
            };

            string updateQuery = string.Format("UPDATE \"KatRejony\" SET rejon = '{0}', nazwa = '{1}', idoper = '{2}', idakcept = '{3}', firma = '{4}', datamodify = '{5}', dataakcept = '{6}', mnemonik = '{7}', systembazowy = '{8}', usuniety = '{9}' WHERE firma = '" + rejonDoEdycji.Firma + "' AND rejon = '" + rejonPrzedZmiana + "'", values);

            IConnectionState connection = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connection.ExecuteNonQuery(updateQuery);

            return result;
        }

    }



}

