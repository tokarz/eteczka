using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using System.Configuration;

namespace Eteczka.BE.Services
{
    public class PlikiService : IPlikiService
    {
        private PlikiDAO _Dao;

        public List<KatTeczki> PobierzWszystkie(string sortOrder = "asc", string sortColumn = "Id")
        {

            string user = ConfigurationManager.AppSettings["dbuser"];
            string password = ConfigurationManager.AppSettings["dbpassword"];

            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            IConnectionDetails connectionDetails = new ConnectionDetails(user, password, host, port, name);
            IDbConnectionFactory factory = new DbConnectionFactory(connectionDetails);

            _Dao = new PlikiDAO(factory);

            List<KatTeczki> pobrane = _Dao.PobierzWszystkiePliki(sortOrder, sortColumn);

            return pobrane;
        }

        public List<KatTeczki> PobierzDlaUzytkownika(string userId, string sortOdred = "asc", string sortColumn = "Id")
        {
            List<KatTeczki> result = new List<KatTeczki>();

            return result;
        }

        public List<KatTeczki> PobierzZawierajaceTekst(string searchText, string sortOrder = "asc", string sortColumn = "Id")
        {
            List<KatTeczki> result = new List<KatTeczki>();

            return result;
        }
    }
}
