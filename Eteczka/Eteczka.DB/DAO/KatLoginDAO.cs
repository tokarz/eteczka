using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using System.Data;

namespace Eteczka.DB.DAO
{
    public class KatLoginDAO
    {
        private IDbConnectionFactory _ConnectionFactory;

        public KatLoginDAO(IDbConnectionFactory factory)
        {
            this._ConnectionFactory = factory;
        }

        public List<KatLoginy> WczytajPracownikaPoNazwieIHasle(string username, string password)
        {
            //SQL Injection Threat!
            string sqlQuery = "SELECT * from \"KatLoginy\" WHERE identyfikator = '" + username + "' and haslolong = '" + password + "';";

            List<KatLoginy> result = new List<KatLoginy>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);

            foreach (DataRow row in queryResult.Rows)
            {
                KatLoginy fetchedResult = new KatLoginy();

                fetchedResult.Id = long.Parse(row[0].ToString());
                fetchedResult.Identyfikator = row[1].ToString();
                fetchedResult.Nazwisko = row[2].ToString();
                fetchedResult.Imie = row[3].ToString();
                fetchedResult.Hasloshort = "(Not_Needed_Here)";
                fetchedResult.Haslolong = "(Authenticated!)";

                fetchedResult.Rolareadonly = bool.Parse(row[6].ToString());
                fetchedResult.Rolaaddpracownik = bool.Parse(row[7].ToString());
                fetchedResult.Rolamodifypracownik = bool.Parse(row[8].ToString());
                fetchedResult.Rolaaddfile = bool.Parse(row[9].ToString());
                fetchedResult.Rolamodifyfile = bool.Parse(row[10].ToString());
                fetchedResult.Rolaslowniki = bool.Parse(row[11].ToString());
                fetchedResult.Rolasendmail = bool.Parse(row[12].ToString());
                fetchedResult.Rolaraport = bool.Parse(row[13].ToString());
                fetchedResult.Rolaraportexport = bool.Parse(row[14].ToString());
                fetchedResult.Roladoubleakcept = bool.Parse(row[15].ToString());

                fetchedResult.Datamodify = DateTime.Parse(row[16].ToString());
                fetchedResult.FirmaSymbol = row[17].ToString();

                result.Add(fetchedResult);

            }

            return result;
        }
    }
}
