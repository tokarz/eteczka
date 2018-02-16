using Eteczka.DB.Connection;
using Eteczka.DB.Mappers;
using Eteczka.Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Eteczka.DB.DAO
{
    public class KoszykDAO : IKoszykDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IConnection _Connection;
        private IPlikiMapper _PlikiMapper;

        public KoszykDAO(IDbConnectionFactory factory, IConnection connection, IPlikiMapper plikiMapper)
        {
            this._ConnectionFactory = factory;
            this._Connection = connection;
            this._PlikiMapper = plikiMapper;
        }

        public bool DodajPlikiDoKoszyka(string firma, KatLoginyDetale aktywnyUser, List<string> plikiId)
        {
            bool result = false;
            StringBuilder batchQuery = new StringBuilder();
            foreach (string id in plikiId)
            {
                string insertQuery = string.Format("INSERT INTO \"Koszyk\" (identyfikator, firma, idpliki) VALUES ('{0}', '{1}', {2});", aktywnyUser.Identyfikator.Trim(), firma.Trim(), id);
                batchQuery.Append(insertQuery);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(batchQuery.ToString());

            return result;
        }

        public int Policz(string firma, KatLoginyDetale user)
        {
            int result = 0;

            string sqlQuery = "SELECT COUNT(*) FROM \"Koszyk\" WHERE firma = '" + firma.Trim() + "' AND identyfikator = '" + user.Identyfikator.Trim() + "';";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable count = connectionState.ExecuteQuery(sqlQuery);
            if (count != null && count.Rows != null && count.Rows.Count > 0)
            {
                result = int.Parse(count.Rows[0][0].ToString());
            }

            return result;
        }

        public List<Pliki> PobierzZawartoscKoszyka(string firma, KatLoginyDetale user)
        {
            List<Pliki> pobranePliki = new List<Pliki>();
            //select * from "Pliki" where id = (select idpliki from "Koszyk" where firma = 'AFM' and identyfikator = 'M.Tokarz')
            string sqlQuery = "select * from \"Pliki\" as pl left join \"KatPracownicy\" as pr on pl.numeread = pr.numeread left join \"KatDokumentyRodzaj\" on pl.symbol = \"KatDokumentyRodzaj\".symbol WHERE id in (select idpliki from \"Koszyk\" where firma = '" + firma + "' and identyfikator = '" + user.Identyfikator + "')";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);

            foreach (DataRow row in result.Rows)
            {
                Pliki fetchedDok = _PlikiMapper.MapujZSql(row);
                pobranePliki.Add(fetchedDok);
            }

            return pobranePliki;
        }

        public bool UsunZKoszyka(string firma, KatLoginyDetale user, List<string> plikiId)
        {
            bool result = false;

            string plikiWherePart = string.Join(",", plikiId);

            string sqlQuery = "DELETE FROM \"Koszyk\" WHERE firma = '" + firma.Trim() + "' AND identyfikator = '" + user.Identyfikator.Trim() + "' AND idpliki in (" + plikiWherePart + ");";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }
        public bool WyczyscKoszyk(string firma, KatLoginyDetale user)
        {
            bool result = false;
            string sqlQuery = "DELETE FROM \"Koszyk\" WHERE firma = '" + firma.Trim() + "' AND identyfikator = '" + user.Identyfikator.Trim() + "';";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

    }
}
