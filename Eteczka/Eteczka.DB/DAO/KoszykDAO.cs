using Eteczka.DB.Connection;
using Eteczka.DB.Mappers;
using Eteczka.Model.Entities;
using System.Collections.Generic;
using System.Data;

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
            string sqlQuery = "select * from \"Pliki\" WHERE id in (select idpliki from \"Koszyk\" where firma = '" + firma + "' and identyfikator = '" + user.Identyfikator + "')";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);

            foreach (DataRow row in result.Rows)
            {
                Pliki fetchedDok = _PlikiMapper.MapujZSql(row);
                pobranePliki.Add(fetchedDok);
            }

            return pobranePliki;
        }
    }
}
