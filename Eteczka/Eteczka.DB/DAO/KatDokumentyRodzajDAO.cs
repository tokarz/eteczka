using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
using Eteczka.DB.Connection;
using System.Data;

namespace Eteczka.DB.DAO
{
    
    public class KatDokumentyRodzajDAO
    {
        private IDbConnectionFactory _ConnectionFactory;

        public KatDokumentyRodzajDAO(IDbConnectionFactory factory)
        {
            this._ConnectionFactory = factory;
        }

        public List<KatDokumentyRodzaj> PobierzWszystkich(string sessionId)
        {
            string sqlQuery = "SELECT * from KatDokumentyRodzaj";

            List<KatDokumentyRodzaj> fetchedResult = new List<KatDokumentyRodzaj>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                KatDokumentyRodzaj fetchedDok = new KatDokumentyRodzaj();
                fetchedDok.Id = long.Parse(row[0].ToString());
                fetchedDok.Nazwa = row[1].ToString();
                fetchedDok.Symbol = row[2].ToString();

                fetchedResult.Add(fetchedDok);
            }

            return fetchedResult;

        }

    }
}
