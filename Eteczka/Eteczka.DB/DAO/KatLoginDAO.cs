using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
using System.Data;
using Eteczka.DB.Mappers;

namespace Eteczka.DB.DAO
{
    public class KatLoginDAO
    {
        private IDbConnectionFactory _ConnectionFactory;
        private IKatLoginyMapper _KatLoginyMapper;

        public KatLoginDAO(IDbConnectionFactory factory, IKatLoginyMapper mapper)
        {
            this._ConnectionFactory = factory;
            this._KatLoginyMapper = mapper;
        }

        public KatLoginy WczytajPracownikaPoNazwieIHasle(string username, string password)
        {
            //SQL Injection Threat!
            string sqlQuery = "SELECT * from \"KatLoginy\" WHERE identyfikator = '" + username + "' and haslolong = '" + password + "';";


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            KatLoginy fetchedResult = this._KatLoginyMapper.Map(queryResult);
            

            return fetchedResult;
        }

        public KatLoginyDetale WczytajDetaleDlaUzytkownika(string id)
        {
            string sqlQuery = "SELECT * from \"KatLoginyDetale\" WHERE identyfikator = '" + id + "';";


            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable queryResult = connectionState.ExecuteQuery(sqlQuery);
            KatLoginyDetale fetchedResult = this._KatLoginyMapper.MapDetails(queryResult);


            return fetchedResult;
        }
    }
}
