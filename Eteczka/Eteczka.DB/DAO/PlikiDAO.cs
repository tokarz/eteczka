using System;
using System.Collections.Generic;
using System.Text;
using Eteczka.Model.Entities;
using System.Data;
using Eteczka.DB.Connection;
using Eteczka.DB.Mappers;
using Eteczka.Model.DTO;
using System.IO;
using System.Globalization;


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

        public bool KomitujPlikDoBazy(KomitPliku plik, string pierwotnaNazwaPliku, string nazwaPliku, string katalogDocelowy, string plikZrodlowy, string firma, string idOper)
        {
            bool result = false;

            try
            {
                if (plik != null)
                {
                    string dataWytworzenia = ParsujDate(plik.DataWytworzenia);
                    string dataPocz = ParsujDate(plik.DataPocz);
                    string dataKoniec = ParsujDate(plik.DataKoniec);
                    string dataSkanu = ParsujDate(File.GetCreationTime(plikZrodlowy));

                    object[] args = new object[]  {
                            firma.Trim(),
                            plik.Pracownik.Numeread.Trim(),
                            plik.Typ.Symbol.Trim(),
                            dataSkanu,
                            dataWytworzenia,
                            dataPocz,
                            dataKoniec,
                            pierwotnaNazwaPliku,
                            nazwaPliku,
                            Path.Combine(katalogDocelowy, nazwaPliku),
                            "pdf",
                            plik.OpisDodatkowy,
                            plik.Dokwlasny,
                            "EAD",
                            false,
                            idOper.Trim(),
                            idOper.Trim(),
                            DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                            plik.Typ.Teczkadzial.Trim(),
                            plik.Typ.SymbolEad.Trim(),
                            plik.NrDokumentu

                        };

                    string values = string.Format("'{0}', '{1}', '{2}', '{3}','{4}', '{5}','{6}', '{7}','{8}', '{9}','{10}', '{11}','{12}', '{13}','{14}', '{15}','{16}', '{17}', '{18}', '{19}','{20}', '{21}', '{22}'", args);
                    string insertStatement = "INSERT INTO \"Pliki\" (firma, numeread, symbol, dataskanu, datadokumentu, datapocz, datakoniec, nazwascan, nazwaead, pelnasciezkaead, typpliku, opisdodatkowy, dokwlasny, systembazowy, usuniety, idoper, idakcept, datadodania, datamodify, dataakcept, teczkadzial, symbolead, nrdokumentu) VALUES (" + values + ");";

                    IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                    result = connectionState.ExecuteNonQuery(insertStatement);
                }

            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool EdytujPlikWBazie(KomitPliku plik, string idoper, string idakcept, string id)
        {
            bool result = false;

            try
            {
                if (plik != null)
                {
                    string dataWytworzenia = ParsujDate(plik.DataWytworzenia);
                    string dataPocz = ParsujDate(plik.DataPocz);
                    string dataKoniec = ParsujDate(plik.DataKoniec);

                    object[] obs = new object[]
                        {
                        plik.Typ.Symbol.Trim(),
                        plik.Dokwlasny,
                        dataWytworzenia,
                        dataPocz,
                        dataKoniec,
                        plik.OpisDodatkowy.Trim(),
                        plik.NrDokumentu,
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        idoper.Trim(),
                        idakcept.Trim(),
                        plik.Pracownik.Numeread.Trim(),
                        id.Trim(),
                        plik.Typ.Teczkadzial.Trim()
                        };

                    string updateQuery = string.Format("UPDATE \"Pliki\" SET " + "symbol='{0}', dokwlasny='{1}', datadokumentu='{2}', datapocz='{3}', datakoniec='{4}', opisdodatkowy='{5}', nrdokumentu='{6}', datamodify='{7}', dataakcept='{8}', idoper='{9}', idakcept='{10}', numeread='{11}', teczkadzial='{13}' WHERE id = '{12}'", obs);

                    IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                    result = connectionState.ExecuteNonQuery(updateQuery);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;

        }

        public bool UsunDokumentyZBazy(List<string> ids, string idoper, string idakcept)
        {
            bool result = false;
            try
            {
                string listaId = string.Join(",", ids);

                object[] obs = new object[]
                {
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        idoper.Trim(),
                        idakcept.Trim(),
                        listaId,
                };

                string updateQuery = string.Format("UPDATE \"Pliki\" SET usuniety = 'TRUE', datamodify = '{0}', dataakcept = '{1}', idoper = '{2}', idakcept = '{3}' where id in ({4})", obs);
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                result = connectionState.ExecuteNonQuery(updateQuery);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool UsunDokumentZBazy(string id, string idoper, string idakcept)
        {
            bool result = false;
            try
            {
                object[] obs = new object[]
                {
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        idoper.Trim(),
                        idakcept.Trim(),
                        id.Trim()
                };

                string updateQuery = string.Format("UPDATE \"Pliki\" SET usuniety = 'TRUE', datamodify = '{0}', dataakcept = '{1}', idoper = '{2}', idakcept = '{3}' where id = '{4}'", obs);
                IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
                result = connectionState.ExecuteNonQuery(updateQuery);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        private string ParsujDate(DateTime data, string format = "yyyy-MM-dd")
        {
            string result = "";
            try
            {
                if (data == DateTime.MinValue)
                {
                    result = "9999-12-31";
                }
                else
                {
                    result = data.ToString(format, CultureInfo.InvariantCulture);
                }
            }
            catch (Exception)
            {
                result = "9999-12-31";
            }

            return result;
        }

        public bool ImportujPliki(Dictionary<string, Pliki> plikiZMetadanymi)
        {
            //Ta metoda chyba nie jest nigdzie używana?
            bool result = false;
            StringBuilder sqls = new StringBuilder();

            foreach (string key in plikiZMetadanymi.Keys)
            {
                Pliki biezacyPlik = plikiZMetadanymi[key];
                string valuesLine = "('" + biezacyPlik.Firma + "', '" + biezacyPlik.NumerEad + "', '" + biezacyPlik.Symbol + "', '" + biezacyPlik.DataSkanu + "', '" + biezacyPlik.DataDokumentu + "', '" + biezacyPlik.DataPocz + "', '" + biezacyPlik.DataKoniec + "', '" + biezacyPlik.NazwaScan + "', '" + biezacyPlik.NazwaEad + "', '" + biezacyPlik.PelnasciezkaEad + "', '" + biezacyPlik.TypPliku + "', '" + biezacyPlik.OpisDodatkowy + "', '" + biezacyPlik.DokumentWlasny + "', '" + biezacyPlik.Systembazowy + "', '" + biezacyPlik.Usuniety + "', '" + biezacyPlik.IdOper + "', '" + biezacyPlik.IdAkcept + "', '" + biezacyPlik.DataModyfikacji + "', '" + biezacyPlik.DataAkcept + "', '" + biezacyPlik.SymbolEad + "');";
                string singleImport = "INSERT INTO \"Pliki\" (firma, numeread, symbol, dataskanu, datadokumentu, datapocz, datakoniec, nazwascan, nazwaead, pelnasciezkaead, typpliku, opisdodatkowy, dokwlasny, systembazowy, usuniety, idoper, idakcept, datamodify, dataakcept, symbolead) VALUES ";

                string fullSqlInsert = singleImport + valuesLine;
                sqls.Append(fullSqlInsert);
            }

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            result = connectionState.ExecuteNonQuery(sqls.ToString());

            return result;
        }

        public List<Pliki> PobierzPlikPoNumerzeEad(string numeread, string firma)
        {
            //string sqlQuery = "SELECT * from \"Pliki\" WHERE numeread = '" + numeread + "';";
            if (numeread == null)
            {
                numeread = "";
            }

            if (firma == null)
            {
                firma = "";
            }

            //string sqlQuery = "SELECT * from \"Pliki\" as pl left join \"KatPracownicy\" as pr on pl.numeread = pr.numeread where pl.firma = '" + firma.Trim() + "' and pr.numeread = '" + numeread + "';";
            string sqlQuery = "SELECT * FROM \"Pliki\" "
                  + "LEFT OUTER JOIN\"KatDokumentyRodzaj\" "
                  + "ON \"Pliki\".symbol = \"KatDokumentyRodzaj\".symbol "
                  + "LEFT OUTER JOIN \"KatPracownicy\" "
                  + " ON \"Pliki\".numeread = \"KatPracownicy\".numeread "
                  + "WHERE \"Pliki\".firma = '" + firma.Trim() + "' "
                  + "AND \"Pliki\".usuniety = 'FALSE'"
                  + "AND \"KatPracownicy\".numeread = '" + numeread + "' "
                  + "ORDER BY \"Pliki\".numeread,\"Pliki\".teczkadzial,SUBSTRING(nrdokumentu FROM '([0-9]+)')::int, nrdokumentu;";

            //sqlQuery += " order by " + sortColumn + " " + sortOrder + ";";

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
            //Zla Kwerenda, NIEUZYWANE
            string sqlQuery = "SELECT * from \"Pliki\" WHERE nazwapliku = '" + nazwa + "';";

            Pliki fetchedDok = new Pliki()
            {
                Imie = "BLAD!"
            };

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            if (result.Rows.Count == 1)
            {
                //fetchedDok = _PlikiMapper.MapujZSql(result.Rows[0]);
            }

            return fetchedDok;
        }

        public List<Pliki> WyszukajPlikiZFiltrow(string firma, string rejon, string wydzial, string podwydzial, string konto5, string typ, string numeread, string date1, string date2, string dateType, string sortOrder, string sortColumn)
        {
            //TODO: Tutaj kwerenda Paszczaka z filtrami!!
            //string sqlQuery = "SELECT * from \"Pliki\";";

            //string sqlQuery =
            //"SELECT * FROM \"Pliki\" " +
            // "WHERE " +
            // "symbol LIKE '" + typ.Trim() + "' " +
            // "AND numeread IN " +
            // "(SELECT numeread FROM \"MiejscePracy\" " +
            // "WHERE NOT \"MiejscePracy\".usuniety " +
            // "AND \"MiejscePracy\".firma IN ('" + firma.Trim() + "') " +
            // "AND rejon LIKE '" + rejon.Trim() + "' " +
            //"AND wydzial LIKE '" + wydzial.Trim() + "' " +
            // "AND podwydzial LIKE '" + podwydzial.Trim() + "' " +
            // "AND konto5 LIKE '" + konto5.Trim() + "' );";


            string sqlQuery =
            "SELECT \"Pliki\".*, \"Pliki\".teczkadzial as numerdzialu, \"KatPracownicy\".*, \"KatDokumentyRodzaj\".* FROM \"Pliki\" "
            + "LEFT OUTER JOIN \"KatPracownicy\" "
            + "ON \"Pliki\".numeread = \"KatPracownicy\".numeread "
              + "LEFT OUTER JOIN \"KatDokumentyRodzaj\" "
            + "ON \"Pliki\".symbol = \"KatDokumentyRodzaj\".symbol "
            + "WHERE "
            + "\"Pliki\".firma IN ('" + firma.Trim() + "') "
            + "AND \"Pliki\".symbolead LIKE '" + typ.Trim() + "' "
            + "AND \"Pliki\".usuniety = 'FALSE' "
            + "AND \"Pliki\".numeread IN "
            + "(SELECT numeread FROM \"MiejscePracy\" "
            + "WHERE NOT \"MiejscePracy\".usuniety "
            + "AND \"MiejscePracy\".firma IN ('" + firma.Trim() + "') "
            + "AND rejon LIKE '" + rejon.Trim() + "' "
            + "AND wydzial LIKE '" + wydzial.Trim() + "' "
            + "AND podwydzial LIKE '" + podwydzial.Trim() + "' "
            + "AND konto5 LIKE '" + konto5.Trim() + "' ) "
            + "AND \"Pliki\".numeread LIKE '" + numeread.Trim() + "'"
            + "AND " + dateType + " BETWEEN'" + date1 + "'AND'" + date2 + "'"
            + "ORDER BY nazwisko, imie, numerdzialu, SUBSTRING(nrdokumentu FROM '([0-9]+)')::int, nrdokumentu "
            + ";";

            List<Pliki> fetchedResult = new List<Pliki>();

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                Pliki fetchedDok = _PlikiMapper.MapujZSql(row);
                if (fetchedDok != null)
                {
                    fetchedResult.Add(fetchedDok);
                }
            }

            return fetchedResult;
        }

        public List<Pliki> ZnajdzOstatnioDodanePlikiPracownika(string numeread, string firma, int liczbaPlikow)
        {
            List<Pliki> ZnalezionePliki = new List<Pliki>();
            string sqlQuery = "SELECT * FROM \"Pliki\" "
            + "LEFT OUTER JOIN \"KatPracownicy\" "
            + "ON \"Pliki\".numeread = \"KatPracownicy\".numeread "
            + "LEFT OUTER JOIN\"KatDokumentyRodzaj\" "
            + "ON \"Pliki\".symbol = \"KatDokumentyRodzaj\".symbol "
            + "WHERE \"Pliki\".numeread = '" + numeread + "' AND firma='" + firma + "' AND \"Pliki\".usuniety = 'FALSE' ORDER BY id DESC LIMIT " + liczbaPlikow + "";

            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);
            foreach (DataRow row in result.Rows)
            {
                Pliki znalezionyDokument = _PlikiMapper.MapujZSql(row);
                if (znalezionyDokument != null)
                {
                    ZnalezionePliki.Add(znalezionyDokument);
                }
            }
            return ZnalezionePliki;
        }

        public int PoliczPlikiPracownikaWTeczce(string numeread, string firma)
        {
            int liczbaPlikow = 0;
            string sqlQuery = "SELECT COUNT(*) FROM \"Pliki\" WHERE numeread='" + numeread + "' AND firma='" + firma + "'";
            IConnectionState connectionState = _ConnectionFactory.CreateConnectionToDB(_Connection);
            DataTable result = connectionState.ExecuteQuery(sqlQuery);

            if (result != null && result.Rows != null && result.Rows.Count > 0)
            {
                liczbaPlikow = int.Parse(result.Rows[0][0].ToString());
            }

            return liczbaPlikow;
        }





    }
}
