using System;
using System.Collections.Generic;
using Eteczka.DB.Entities;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using System.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
using Eteczka.BE.DTO;
using System.Text;
using Eteczka.BE.Utils;

namespace Eteczka.BE.Services
{
    public class ImportService : IImportService
    {
        public ImportResult ImportFiles(bool nadpisz)
        {
            ImportResult result = new ImportResult();
            Dictionary<string, Plik> wczytanePliki = new Dictionary<string, Plik>();
            List<string> znalezionePdf = new List<string>();
            List<string> znalezioneMetaDane = new List<string>();

            PlikiUtils plikiUtils = new PlikiUtils();


            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");

            string sciezkaDoPlikow = Path.Combine(eadRoot, "pliki");

            string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
            foreach (string plik in pliki)
            {
                if (plik.EndsWith(".json"))
                {
                    znalezioneMetaDane.Add(plik);
                }
                else
                {
                    znalezionePdf.Add(plik);
                }
            }

            foreach (string metaDanePliku in znalezioneMetaDane)
            {
                string metaDaneBezRozszerzenia = metaDanePliku.Substring(0, metaDanePliku.Length - 5);
                foreach (string dokument in znalezionePdf)
                {
                    string dokumentBezRozszerzenia = dokument.Substring(0, dokument.Length - 4);
                    if (metaDaneBezRozszerzenia.Equals(dokumentBezRozszerzenia))
                    {
                        Plik metaDaneJson = new Plik();
                        StringBuilder jsonMetadataFile = new StringBuilder();

                        using (StreamReader reader = new StreamReader(metaDanePliku))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                jsonMetadataFile.Append(line);
                            }


                            var rootJson = JObject.Parse(jsonMetadataFile.ToString());
                            var parsedJson = rootJson["file"];
                            metaDaneJson.Id = parsedJson["id"].ToString();
                            metaDaneJson.Jrwa = parsedJson["jrwa"].ToString();
                            metaDaneJson.Nazwa = parsedJson["name"].ToString() + "." + parsedJson["ext"].ToString();
                            metaDaneJson.TypDokumentu = parsedJson["type"].ToString();
                            metaDaneJson.DataUtworzenia = DateTime.Parse(parsedJson["createdate"].ToString());
                            metaDaneJson.DataModyfikacji = DateTime.Parse(parsedJson["lastchangedate"].ToString());
                            metaDaneJson.Komentarz = parsedJson["comment"].ToString();
                            metaDaneJson.Pesel = parsedJson["pesel"].ToString();

                        }


                        wczytanePliki.Add(dokument, metaDaneJson);
                    }
                }
            }

            string user = ConfigurationManager.AppSettings["dbuser"];
            string password = ConfigurationManager.AppSettings["dbpassword"];
            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            IConnectionDetails connectionDetails = new ConnectionDetails(user, password, host, port, name);
            IDbConnectionFactory factory = new DbConnectionFactory(connectionDetails);

            PlikiDAO dao = new PlikiDAO(factory);
            result.ImportSukces = dao.ImportujPliki(wczytanePliki);



            return result;
        }

        public ImportResult ImportArchives(bool nadpisz)
        {
            ImportResult result = new ImportResult();

            List<KatLokalPapier> lokalneArchiwum = new List<KatLokalPapier>();
            List<string> filePaths = new List<string>();
            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");

            string sciezkaDoPlikow = Path.Combine(eadRoot, "zet", "Archiwa");

            string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
            StringBuilder jsonFile = new StringBuilder();
            foreach (string plik in pliki)
            {
                jsonFile.Clear();
                using (StreamReader reader = new StreamReader(plik))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        jsonFile.Append(line);
                    }


                    var rootJson = JObject.Parse(jsonFile.ToString());
                    var parsedArray = rootJson["arch"];

                    foreach (var parsedJson in parsedArray)
                    {
                        KatLokalPapier aktualneArchiwumDoWczytania = new KatLokalPapier();
                        aktualneArchiwumDoWczytania.Id = long.Parse(parsedJson["id"].ToString());
                        aktualneArchiwumDoWczytania.Symbolfirma = parsedJson["symbolfirma"].ToString();
                        aktualneArchiwumDoWczytania.Symbol = parsedJson["symbol"].ToString();
                        aktualneArchiwumDoWczytania.Nazwa = parsedJson["nazwa"].ToString();
                        aktualneArchiwumDoWczytania.Ulica = parsedJson["ulica"].ToString();
                        aktualneArchiwumDoWczytania.Numerdomu = parsedJson["numerdomu"].ToString();
                        aktualneArchiwumDoWczytania.Numerlokalu = parsedJson["numerlokalu"].ToString();
                        aktualneArchiwumDoWczytania.Kodpocztowy = parsedJson["kodpocztowy"].ToString();
                        aktualneArchiwumDoWczytania.Miasto = parsedJson["miasto"].ToString();
                        aktualneArchiwumDoWczytania.Poczta = parsedJson["poczta"].ToString();

                        lokalneArchiwum.Add(aktualneArchiwumDoWczytania);
                    }
                }
            }


            string user = ConfigurationManager.AppSettings["dbuser"];
            string password = ConfigurationManager.AppSettings["dbpassword"];
            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            IConnectionDetails connectionDetails = new ConnectionDetails(user, password, host, port, name);
            IDbConnectionFactory factory = new DbConnectionFactory(connectionDetails);

            PlikiDAO dao = new PlikiDAO(factory);
            result.ImportSukces = dao.ImportujArchiwa(lokalneArchiwum);


            return result;
        }




        public ImportResult ImportFirms(bool nadpisz)
        {
            ImportResult result = new ImportResult();

            List<KatFIrmy> lokalneFirmy = new List<KatFIrmy>();
            List<string> filePaths = new List<string>();
            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");

            string sciezkaDoPlikow = Path.Combine(eadRoot, "zet", "Firmy");

            string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
            StringBuilder jsonFile = new StringBuilder();
            foreach (string plik in pliki)
            {
                jsonFile.Clear();
                using (StreamReader reader = new StreamReader(plik))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        jsonFile.Append(line);
                    }


                    var rootJson = JObject.Parse(jsonFile.ToString());
                    var parsedArray = rootJson["firm"];

                    foreach (var parsedJson in parsedArray)
                    {
                        KatFIrmy aktualnaFirma = new KatFIrmy();

                        aktualnaFirma.Id = long.Parse(parsedJson["id"].ToString());
                        aktualnaFirma.Symbol = parsedJson["symbol"].ToString();
                        aktualnaFirma.Nazwa = parsedJson["nazwa"].ToString();
                        aktualnaFirma.Nazwaskrocona = parsedJson["nazwaskrocona"].ToString();
                        aktualnaFirma.Ulica = parsedJson["ulica"].ToString();
                        aktualnaFirma.Numerdomu = parsedJson["numerdomu"].ToString();
                        aktualnaFirma.Numerlokalu = parsedJson["numerlokalu"].ToString();
                        aktualnaFirma.Miasto = parsedJson["miasto"].ToString();
                        aktualnaFirma.Kodpocztowy = parsedJson["kodpocztowy"].ToString();
                        aktualnaFirma.Poczta = parsedJson["poczta"].ToString();
                        aktualnaFirma.Gmina = parsedJson["gmina"].ToString();
                        aktualnaFirma.Powiat = parsedJson["powiat"].ToString();
                        aktualnaFirma.Wojewodztwo = parsedJson["wojewodztwo"].ToString();
                        aktualnaFirma.Kraj = parsedJson["kraj"].ToString();
                        aktualnaFirma.Nip = parsedJson["nip"].ToString();
                        aktualnaFirma.Regon = parsedJson["regon"].ToString();
                        aktualnaFirma.Pesel = parsedJson["pesel"].ToString();
                        aktualnaFirma.Lokalizacjapapier = parsedJson["lokalizacjapapier"].ToString();

                        aktualnaFirma.Datamodify = DateTime.Now;
                        aktualnaFirma.Idoper = 0;
                        aktualnaFirma.Idakcept = 0;
                        aktualnaFirma.Dataakcept = DateTime.Now;

                        lokalneFirmy.Add(aktualnaFirma);
                    }
                }
            }


            string user = ConfigurationManager.AppSettings["dbuser"];
            string password = ConfigurationManager.AppSettings["dbpassword"];
            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            IConnectionDetails connectionDetails = new ConnectionDetails(user, password, host, port, name);
            IDbConnectionFactory factory = new DbConnectionFactory(connectionDetails);

            PlikiDAO dao = new PlikiDAO(factory);
            result.ImportSukces = dao.ImportujFirmy(lokalneFirmy);


            return result;

        }
        public ImportResult ImportAreas(bool nadpisz)
        {
            ImportResult result = new ImportResult();

            List<KatRejony> lokalneRejony = new List<KatRejony>();
            List<string> filePaths = new List<string>();
            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");

            string sciezkaDoPlikow = Path.Combine(eadRoot, "zet", "Rejony");

            string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
            StringBuilder jsonFile = new StringBuilder();
            foreach (string plik in pliki)
            {
                jsonFile.Clear();
                using (StreamReader reader = new StreamReader(plik))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        jsonFile.Append(line);
                    }


                    var rootJson = JObject.Parse(jsonFile.ToString());
                    var parsedArray = rootJson["area"];

                    foreach (var parsedJson in parsedArray)
                    {
                        KatRejony aktualnyRejon = new KatRejony();

                        aktualnyRejon.Id = long.Parse(parsedJson["id"].ToString());
                        aktualnyRejon.Symbol = parsedJson["symbol"].ToString();
                        aktualnyRejon.Nazwa= parsedJson["nazwa"].ToString();
                        aktualnyRejon.Lokalizacjapapier = "jw";
                        aktualnyRejon.FirmaId = long.Parse(parsedJson["firma"].ToString()); ;

                        aktualnyRejon.Datamodify = DateTime.Now;
                        aktualnyRejon.Idoper = 0;
                        aktualnyRejon.Idakcept = 0;
                        aktualnyRejon.Dataakcept = DateTime.Now;


                        lokalneRejony.Add(aktualnyRejon);
                    }
                }
            }


            string user = ConfigurationManager.AppSettings["dbuser"];
            string password = ConfigurationManager.AppSettings["dbpassword"];
            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            IConnectionDetails connectionDetails = new ConnectionDetails(user, password, host, port, name);
            IDbConnectionFactory factory = new DbConnectionFactory(connectionDetails);

            PlikiDAO dao = new PlikiDAO(factory);
            result.ImportSukces = dao.ImportujRejony(lokalneRejony);


            return result;
        }
    }
}
