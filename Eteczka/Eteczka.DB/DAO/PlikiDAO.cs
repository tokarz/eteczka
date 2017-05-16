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

        public bool ImportujPliki(Dictionary<string, Plik> plikiZMetadanymi)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();


            foreach (string key in plikiZMetadanymi.Keys)
            {
                Plik biezacyPlik = plikiZMetadanymi[key];
                string valuesLine = "(" + biezacyPlik.Id + ", '" + biezacyPlik.Nazwa + "', 'pelnaSciezka', '" + biezacyPlik.Jrwa + "', 0,'" + biezacyPlik.DataUtworzenia + "', '" + biezacyPlik.DataModyfikacji + "', '" + biezacyPlik.TypDokumentu + "');";
                string singleImport = "INSERT INTO \"KatTeczki\" (id, nazwa, pelna_sciezka, jrwa, jrwa_id, data_utworzenia, data_modyfikacji, typid) VALUES ";

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
            long startingId = archiwa[0].Id;

            foreach (KatLokalPapier biezacyPlik in archiwa)
            {
                string valuesLine = "(" + startingId++ + ", '" + biezacyPlik.Symbolfirma + "', '" + biezacyPlik.Symbol + "','" + biezacyPlik.Nazwa + "','" + biezacyPlik.Ulica + "','" + biezacyPlik.Numerdomu + "','" + biezacyPlik.Numerlokalu + "','" + biezacyPlik.Miasto + "','" + biezacyPlik.Kodpocztowy + "','" + biezacyPlik.Poczta + "','" + DateTime.Now + "','0', '0', '" + DateTime.Now + "');";
                string singleImport = "INSERT INTO \"KatLokalPapier\"(id, symbolfirma, symbol, nazwa, ulica, numerdomu, numerlokalu, miasto, kodpocztowy, poczta, datamodify, idoper, idakcept, dataakcept) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public bool ImportujFirmy(List<KatFIrmy> firmy)
        {
            bool result = false;
            StringBuilder sqls = new StringBuilder();
            long startingId = firmy[0].Id;

            foreach (KatFIrmy biezacyPlik in firmy)
            {
                string valuesLine = "(" + startingId++ + ", '" + biezacyPlik.Symbol + "', '" + biezacyPlik.Nazwa + "','" + biezacyPlik.Nazwaskrocona + "', '" + biezacyPlik.Ulica + "','" + biezacyPlik.Numerdomu + "','" + biezacyPlik.Numerlokalu + "','" + biezacyPlik.Miasto + "','" + biezacyPlik.Kodpocztowy + "','" + biezacyPlik.Poczta + "','" + biezacyPlik.Gmina + "','" + biezacyPlik.Powiat + "', '" + biezacyPlik.Wojewodztwo + "', '" + biezacyPlik.Kraj + "', '" + biezacyPlik.Nip + "', '" + biezacyPlik.Regon + "', '" + biezacyPlik.Kraj +
                    "', '" + biezacyPlik.Pesel + "', '" + biezacyPlik.Datamodify + "', '" + biezacyPlik.Idoper + "', '" + biezacyPlik.Idakcept + "', '" + biezacyPlik.Dataakcept + "', '" + biezacyPlik.Lokalizacjapapier + "');";
                string singleImport = "INSERT INTO \"KatFirmy\"(id, symbol, nazwa, nazwaskrocona, ulica, numerdomu, numerlokalu, miasto, kodpocztowy, poczta, gmina, powiat, wojewodztwo, kraj, nip, regon, krs, pesel, datamodify, idoper, idakcept, dataakcept, lokalizacjapapier) VALUES ";

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
            long startingId = rejony[0].Id;

            foreach (KatRejony biezacyPlik in rejony)
            {
                string valuesLine = "(" + startingId++ + ", '" + biezacyPlik.Symbol + "', '" + biezacyPlik.Nazwa + "','" + biezacyPlik.Idoper + "','" + biezacyPlik.Idakcept + "','" + biezacyPlik.Dataakcept + "','" + biezacyPlik.Lokalizacjapapier + "','" + biezacyPlik.Datamodify + "','" + biezacyPlik.FirmaId + "');";
                string singleImport = "INSERT INTO \"KatRejony\"(id, symbol, nazwa, idoper, idakcept, dataakcept, lokalizacjapapier, datamodify, firmaid) VALUES";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(new Eteczka.DB.Connection.Connection());
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }
    }
}
