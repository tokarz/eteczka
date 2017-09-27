using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using System.Data;
using Eteczka.DB.Connection;
using System.Collections.Generic;
using Eteczka.DB.Mappers;


namespace Eteczka.DB.DAO
{
    public class PlikiDAO
    {
        private IPlikiMapper _PlikiMapper;
        private IDbConnectionFactory _ConnectionFactory;
        private IConnection _Connection;

        public PlikiDAO(IDbConnectionFactory factory, IPlikiMapper plikiMapper, IConnection connection)
        {
            this._ConnectionFactory = factory;
            this._PlikiMapper = plikiMapper;
            this._Connection = connection;
        }

        public List<Pliki> PobierzWszystkiePliki(string order, string column)
        {
            string sqlQuery = "SELECT * from \"Pliki\" order by " + column + " " + order;

            List<Pliki> fetchedResult = new List<Pliki>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                Pliki fetchedDok = _PlikiMapper.MapujZSql(row);
                fetchedResult.Add(fetchedDok);
            }

            return fetchedResult;
        }

        public bool ImportujPliki(Dictionary<string, Pliki> plikiZMetadanymi)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (string key in plikiZMetadanymi.Keys)
            {
                Pliki biezacyPlik = plikiZMetadanymi[key];
                string valuesLine = "('" + biezacyPlik.Firma + "', '" + biezacyPlik.NumerEad + "', '" + biezacyPlik.Symbol + "', '" + biezacyPlik.DataSkanu + "', '" + biezacyPlik.DataDokumentu + "', '" + biezacyPlik.DataPocz + "', '" + biezacyPlik.DataKoniec + "', '" + biezacyPlik.NazwaScan + "', '" + biezacyPlik.NazwaEad + "', '" + biezacyPlik.PelnasciezkaEad + "', '" + biezacyPlik.TypPliku + "', '" + biezacyPlik.OpisDodatkowy + "', '" + biezacyPlik.DokumentWlasny + "', '" + biezacyPlik.Systembazowy + "', '" + biezacyPlik.Usuniety + "', '" + biezacyPlik.IdOper + "', '" + biezacyPlik.IdAkcept + "', '" + biezacyPlik.DataModyfikacji + "', '" + biezacyPlik.DataAkcept + "');";
                string singleImport = "INSERT INTO \"Pliki\" (firma, numeread, symbol, dataskanu, datadokumentu, datapocz, datakoniec, nazwascan, nazwaead, pelnasciezkaead, typpliku, opisdodatkowy, dokwlasny, systembazowy, usuniety, idoper, idakcept, datamodify, dataakcept) VALUES ";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public List<Pliki> PobierzPlikPoNumerzeEad(string numeread)
        {
            string sqlQuery = "SELECT * from \"Pliki\" WHERE numeread = '" + numeread + "';";

            List<Pliki> fetchedResult = new List<Pliki>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                Pliki fetchedDok = _PlikiMapper.MapujZSql(row);
                fetchedResult.Add(fetchedDok);
            }

            return fetchedResult;
        }

        public Pliki PobierzPlikPoNazwie(string nazwa)
        {
            string sqlQuery = "SELECT * from \"Pliki\" WHERE nazwapliku = '" + nazwa + "';";

            Pliki fetchedDok = new Pliki();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            if (result.Rows.Count == 1)
            {
                fetchedDok = _PlikiMapper.MapujZSql(result.Rows[0]);
            }

            return fetchedDok;
        }
    }
}
