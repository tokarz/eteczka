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


        public bool ImportujArchiwa(List<KatLokalPapier> archiwa)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (KatLokalPapier biezacyPlik in archiwa)
            {
                string valuesLine = "('" + biezacyPlik.Firma + "', '" + biezacyPlik.LokalPapier + "','" + biezacyPlik.Nazwa + "','" + biezacyPlik.Ulica + "','" + biezacyPlik.Numerdomu + "','" + biezacyPlik.Numerlokalu + "','" + biezacyPlik.Miasto + "','" + biezacyPlik.Kodpocztowy + "','" + biezacyPlik.Poczta + "','" + biezacyPlik.Idoper + "','" + biezacyPlik.Idakcept + "','" + biezacyPlik.Datamodify + "', '" + biezacyPlik.Dataakcept + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatLokalPapier\"(firma, lokalpapier, nazwa, ulica, numerdomu, numerlokalu, miasto, kodpocztowy, poczta, idoper, idakcept, datamodify, dataakcept, systembazowy, usuniety) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public bool ImportujFirmy(List<KatFirmy> firmy)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (KatFirmy biezacyPlik in firmy)
            {
                string valuesLine = "('" + biezacyPlik.Firma + "', '" + biezacyPlik.Nazwa + "','" + biezacyPlik.Nazwaskrocona + "', '" + biezacyPlik.Ulica + "','" + biezacyPlik.Numerdomu + "','" + biezacyPlik.Numerlokalu + "','" + biezacyPlik.Miasto + "','" + biezacyPlik.Kodpocztowy + "','" + biezacyPlik.Poczta + "','" + biezacyPlik.Gmina + "','" + biezacyPlik.Powiat + "', '" + biezacyPlik.Wojewodztwo + "', '" + biezacyPlik.Nip + "', '" + biezacyPlik.Regon + "', '" + biezacyPlik.Nazwa2 +
                    "', '" + biezacyPlik.Pesel + "', '" + biezacyPlik.Idoper + "', '" + biezacyPlik.Idakcept + "', '" + biezacyPlik.Nazwisko + "', '" + biezacyPlik.Imie + "', '" + biezacyPlik.Datamodify + "', '" + biezacyPlik.Dataakcept + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatFirmy\"(firma, nazwa, nazwaskrocona, ulica, numerdomu, numerlokalu, miasto, kodpocztowy, poczta, gmina, powiat, wojewodztwo, nip, regon, nazwa2, pesel, idoper, idakcept, nazwisko, imie, datamodify, dataakcept, systembazowy, usuniety) VALUES ";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public bool ImportujRejony(List<KatRejony> rejony)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (KatRejony biezacyPlik in rejony)
            {
                string valuesLine = "('" + biezacyPlik.Rejon + "', '" + biezacyPlik.Nazwa + "','" + biezacyPlik.Idoper + "','" + biezacyPlik.Idakcept + "','" + biezacyPlik.Firma + "','" + biezacyPlik.Datamodify + "','" + biezacyPlik.Dataakcept + "','" + biezacyPlik.Mnemonik + "', 'EAD', 'false');";
                string singleImport = "INSERT INTO \"KatRejony\"(rejon, nazwa, idoper, idakcept, firma, datamodify, dataakcept, mnemonik, systembazowy, usuniety) VALUES";

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
