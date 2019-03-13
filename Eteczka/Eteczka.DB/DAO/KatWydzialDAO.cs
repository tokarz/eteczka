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
    public class KatWydzialDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IKatWydzialMapper _KatWydzialMapper;
        private IConnection _Connection;

        public KatWydzialDAO(IDbConnectionFactory factory, IKatWydzialMapper WydzialMapper, IConnection connection)
        {
            this._ConnectionFactory = factory;
            this._KatWydzialMapper = WydzialMapper;
            this._Connection = connection;
        }

        public bool ImportujWydzialy(List<KatWydzialy> dzialy)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (KatWydzialy biezacyDzial in dzialy)
            {
                string valuesLine = "('" + biezacyDzial.Wydzial + "', '" + biezacyDzial.Nazwa + "','" + biezacyDzial.Datamodify + "','" + biezacyDzial.Idoper + "','" + biezacyDzial.Idakcept + "','" + biezacyDzial.Dataakcept + "','" + biezacyDzial.Firma + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatWydzial\"(wydzial, nazwa, datamodify, idoper, idakcept, dataakcept, firma, systembazowy, usuniety) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public int PoliczWydzialyWBazie()
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"KatWydzial\"; ";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }

        public List <KatWydzialy> PobierzDlaFirmy(string firma)
        {
            List<KatWydzialy> PobraneWydzialyDlaFirmy = new List<KatWydzialy>();
            
            string sqlQuery = "SELECT *  FROM \"KatWydzial\" WHERE LOWER(\"KatWydzial\".firma) in ('" + firma.ToLower().Trim() + "') ORDER BY firma;";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);

            foreach (DataRow row in result.Rows)
            {
                KatWydzialy PobranyWydzial = _KatWydzialMapper.MapujzSql(row);
                PobraneWydzialyDlaFirmy.Add(PobranyWydzial);
            }

            return PobraneWydzialyDlaFirmy;
        }

        public bool SprawdzCzyWydzialIstniejeWFirmie(string firma, string wydzial)
        {
            bool result = false;
            int count = 0;

            string sqlQuery = "SELECT COUNT (*) FROM \"KatWydzial\" WHERE firma = '" + firma + "' AND wydzial = '" + wydzial + "'";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable table = connectionState.ExecuteQuery(sqlQuery);

            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                count = int.Parse(table.Rows[0][0].ToString());
            }
            result = count > 0 ? true : false;

            return result;

        }
        public bool DodajWydzialDlaFirmy(KatWydzialy wydzialDoDodania, string idoper, string idakcept)
        {
            bool result = false;

            object[] values = new object[]
                {
                wydzialDoDodania.Wydzial,
                wydzialDoDodania.Nazwa,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                idoper,
                idakcept,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms"),
                wydzialDoDodania.Firma,
                "EAD",
                false
                };
            string sqlQuery = string.Format("INSERT INTO \"KatWydzial\" (wydzial, nazwa, datamodify, idoper, idakcept, dataakcept, firma, systembazowy, usuniety) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')", values);

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }
    }
}
