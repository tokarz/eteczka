using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
using System.Data;
using Eteczka.DB.Connection;
using System.Collections.Generic;

namespace Eteczka.DB.DAO
{
    public class PlikiDAO
    {

        private IDbConnectionFactory _ConnectionFactory;

        public PlikiDAO(IDbConnectionFactory factory)
        {
            this._ConnectionFactory = factory;
        }

        public List<KatTeczki> PobierzWszystkiePliki(string order, string column)
        {
            string sqlQuery = "SELECT * from \"KatTeczki\" order by " + column + " " + order;

            List<KatTeczki> fetchedResult = new List<KatTeczki>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                KatTeczki fetchedDok = new KatTeczki();
                fetchedDok.Id = row[0].ToString();


                fetchedDok.Id = row[0].ToString();
                fetchedDok.Nazwa = row[1].ToString();
                fetchedDok.PelnaSciezka = row[2].ToString();
                fetchedDok.Jrwa = row[3].ToString();
                fetchedDok.JrwaId = row[4].ToString();
                fetchedDok.DataUtworzenia = DateTime.Parse(row[5].ToString());
                fetchedDok.DataModyfikacji = DateTime.Parse(row[6].ToString());
                fetchedDok.TypDokumentu = row[7].ToString();

                fetchedResult.Add(fetchedDok);
            }

            return fetchedResult;
        }

    }
}
