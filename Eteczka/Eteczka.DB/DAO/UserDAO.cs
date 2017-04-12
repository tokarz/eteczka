using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
using Eteczka.DB.Connection;
using System.Collections.Generic;

namespace Eteczka.DB.DAO
{
    public class UserDAO
    {
        private IDbConnectionFactory _ConnectionFactory;

        public UserDAO(IDbConnectionFactory factory)
        {
            this._ConnectionFactory = factory;
        }

        public User GetUserByName(string name)
        {
            string sqlQuery = "SELECT * from users where name = '" + name + "';";
            User fetchedUser = new User();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                fetchedUser.Id = row[0].ToString();
                fetchedUser.Imie = row[1].ToString();
                fetchedUser.Nazwisko = row[2].ToString();
                fetchedUser.PESEL = row[3].ToString();
                fetchedUser.DataUrodzenia = row[4].ToString();
                fetchedUser.Dzial = row[5].ToString();
            }

            return fetchedUser;
        }

        public List<User> GetAllUsers()
        {
            string sqlQuery = "SELECT * from users;";
            List<User> fetchedUsers = new List<User>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                User fetchedUser = new User();
                fetchedUser.Id = row[0].ToString();
                fetchedUser.Imie = row[1].ToString();
                fetchedUser.Nazwisko = row[2].ToString();
                fetchedUser.PESEL = row[3].ToString();
                fetchedUser.DataUrodzenia = row[4].ToString();
                fetchedUser.Dzial = row[5].ToString();

                fetchedUsers.Add(fetchedUser);
            }

            return fetchedUsers;
        }

    }

}
