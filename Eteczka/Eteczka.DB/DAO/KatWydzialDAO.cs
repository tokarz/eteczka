using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
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

        public KatWydzialDAO(IDbConnectionFactory factory, IKatWydzialMapper WydzialMapper)
        {
            this._ConnectionFactory = factory;
            this._KatWydzialMapper = WydzialMapper;
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

        public List <KatWydzialy> PobierzDlaFirmy(string firma)
        {

            List<KatWydzialy> PobraneWydzialyDlaFirmy = new List<KatWydzialy>();

            
            string sqlQuery = "SELECT *  FROM \"KatWydzial\" WHERE LOWER(\"KatWydzial\".firma) in ('" + firma.ToLower().Trim() + "') ORDER BY firma;";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable result = connectionState.ExecuteQuery(sqlQuery);

            foreach (DataRow row in result.Rows)
            {
                KatWydzialy PobranyWydzial = _KatWydzialMapper.MapujzSql(row);
                PobraneWydzialyDlaFirmy.Add(PobranyWydzial);
            }
        
           
            return PobraneWydzialyDlaFirmy;
        }
    }
}
