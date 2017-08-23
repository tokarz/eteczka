using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.Entities;
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

        public PlikiDAO(IDbConnectionFactory factory, IPlikiMapper plikiMapper)
        {
            this._ConnectionFactory = factory;
            this._PlikiMapper = plikiMapper;
        }

        public List<Pliki> PobierzWszystkiePliki(string order, string column)
        {
            string sqlQuery = "SELECT * from \"Pliki\" order by " + column + " " + order;

            List<Pliki> fetchedResult = new List<Pliki>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
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
                string valuesLine = "('" + biezacyPlik.Symbol + "', '" + biezacyPlik.DataSkanu + "', '" + biezacyPlik.DataDokumentu + "', '" + biezacyPlik.DataPocz + "', '" + biezacyPlik.DataKoniec + "', '" + biezacyPlik.NazwaPliku + "', '" + biezacyPlik.PelnaSciezka + "', '" + biezacyPlik.TypPliku + "', '" + biezacyPlik.OpisDodatkowy + "', '" + biezacyPlik.NumerEad + "', '" + biezacyPlik.DokumentWlasny + "', '" + biezacyPlik.IdOper + "', '" + biezacyPlik.IdAkcept + "', '" + biezacyPlik.DataModyfikacji + "', '" + biezacyPlik.DataAkcept + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"Pliki\" (symbol, dataskanu, datadokumentu, datapocz, datakoniec, nazwapliku, pelnasciezka, typpliku, opisdodatkowy, numeread, dokwlasny, idoper, idakcept, datamodify, dataakcept, systembazowy, usuniety) VALUES ";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }



        public Pliki PobierzPlikPoNazwie(string nazwa)
        {
            string sqlQuery = "SELECT * from \"Pliki\" WHERE nazwapliku = '" + nazwa + "';";

            Pliki fetchedDok = new Pliki();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            if (result.Rows.Count == 1)
            {
                fetchedDok = _PlikiMapper.MapujZSql(result.Rows[0]);
            }

            return fetchedDok;
        }
    }
}
