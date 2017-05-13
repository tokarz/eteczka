using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
using System.Data;
using Eteczka.DB.Connection;
using System.Collections.Generic;
using Eteczka.DB.Entities;


namespace Eteczka.DB.DAO
{
    public class PracownikDAO
    {

        private IDbConnectionFactory _ConnectionFactory;

        public PracownikDAO(IDbConnectionFactory factory)
        {
            this._ConnectionFactory = factory;
        }

        public bool ImportujPracownikow(List<Pracownik> pracownicy)
        {

            StringBuilder queries = new StringBuilder();
            foreach (Pracownik pracownik in pracownicy)
            {
                string query = "INSERT INTO \"KatPracownicy\" (id, imie, nazwisko, pesel, dzialid, data_urodzenia, numerpracownika) VALUES (" + pracownik.Id + ", '" + pracownik.Imie + "', '" + pracownik.Nazwisko + "', '" + pracownik.PESEL + "', " + "0" + ", " + "'1999-11-11'," + " '" + pracownik.NumerPracownika + "');";
                queries.Append(query);
            }


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            bool result = connectionState.ExecuteNonQuery(queries.ToString());


            return result;
        }

        public List<Pracownik> PobierzPracownikow()
        {

            string sqlQuery = "SELECT * from KatPracownicy;";
            List<Pracownik> fetchedUsers = new List<Pracownik>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                Pracownik fetchedUser = new Pracownik();
                fetchedUser.Id = row[0].ToString();
                fetchedUser.Imie = row[1].ToString();
                fetchedUser.Nazwisko = row[2].ToString();
                fetchedUser.PESEL = row[3].ToString();
                fetchedUser.Dzial = row[4].ToString();
                fetchedUser.DataUrodzenia = row[5].ToString();

                fetchedUsers.Add(fetchedUser);
            }

            return fetchedUsers;
        }

    }
}
