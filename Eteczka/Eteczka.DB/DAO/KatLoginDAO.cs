using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Connection;
using Eteczka.Model.Entities;
using System.Data;
using Eteczka.DB.Mappers;
using Eteczka.DB.Utils;

namespace Eteczka.DB.DAO
{
    public class KatLoginDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IKatLoginyMapper _KatLoginyMapper;
        private IConnection _Connection;
        private CryptoUtils _Crypto;

        public KatLoginDAO(IDbConnectionFactory factory, IKatLoginyMapper mapper, IConnection connection, CryptoUtils crypto)
        {
            this._ConnectionFactory = factory;
            this._KatLoginyMapper = mapper;
            this._Connection = connection;
            this._Crypto = crypto;
        }

        public bool UsunFirmeUzytkownika(KatLoginy user, string firma)
        {
            string sqlQuery = "UPDATE \"KatLoginyDetale\" SET usuniety=true WHERE identyfikator='" + user.Identyfikator.Trim() + "' and firma='" + firma + "';";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            bool result = connectionState.ExecuteNonQuery(sqlQuery);

            return result;
        }

        public KatLoginy WczytajPracownikaPoNazwieIHasle(string username, string password)
        {
            //SQL Injection Threat!
            string sqlQuery = "SELECT * from \"KatLoginy\" WHERE identyfikator = '" + username + "' and haslolong = '" + _Crypto.CalculateMD5Hash(password) + "';";


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            KatLoginy fetchedResult = this._KatLoginyMapper.Map(queryResult);


            return fetchedResult;
        }

        public List<KatLoginyDetale> WczytajDetaleDlaUzytkownika(string id)
        {
            string sqlQuery = "SELECT * from \"KatLoginyDetale\" WHERE identyfikator = '" + id + "';";


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            List<KatLoginyDetale> fetchedResult = this._KatLoginyMapper.MapDetails(queryResult);


            return fetchedResult;
        }

        public List<KatLoginyDetale> WczytajWszystkieDetale(bool czyAdminTez = false)
        {
            string wherePart = ";";
            if (!czyAdminTez)
            {
                wherePart = " WHERE identyfikator != 'Administrator';";
            }
            string sqlQuery = "SELECT * from \"KatLoginyDetale\"" + wherePart;


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            List<KatLoginyDetale> fetchedResult = this._KatLoginyMapper.MapDetails(queryResult);

            return fetchedResult;
        }
    }
}
