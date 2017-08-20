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
using Eteczka.BE.Mappers;

namespace Eteczka.BE.Services
{
    public class ImportService : IImportService
    {
        private IJsonToKatLokalMapper _JsonToKatLokalMapper;
        private IJsonToKatFirmyMapper _JsonToKatFirmyMapper;
        private IJsonToPlikiMapper _JsonToPlikiMapper;
        private IJsonToKatRejonyMapper _JsonToKatRejonyMapper;
        private IJsonToPracownikMapper _JsonToPracownikMapper;
        private PlikiUtils _PlikiUtils;
        private PlikiDAO _Dao;
        private PracownikDAO _PracownikDao;

        public ImportService(IJsonToKatLokalMapper mapper, IJsonToKatFirmyMapper firmyMapper, IJsonToPlikiMapper plikiMapper, IJsonToKatRejonyMapper rejonyMapper, IJsonToPracownikMapper pracownikMapper, PlikiUtils plikiUtils, PlikiDAO dao, PracownikDAO pracownikDao)
        {
            this._JsonToKatLokalMapper = mapper;
            this._JsonToKatFirmyMapper = firmyMapper;
            this._JsonToPlikiMapper = plikiMapper;
            this._JsonToKatRejonyMapper = rejonyMapper;
            this._JsonToPracownikMapper = pracownikMapper;
            this._PlikiUtils = plikiUtils;
            this._Dao = dao;
            this._PracownikDao = pracownikDao;
        }

        public ImportResult ImportFiles(bool nadpisz)
        {
            ImportResult result = new ImportResult();
            Dictionary<string, Pliki> wczytanePliki = new Dictionary<string, Pliki>();
            List<string> znalezionePdf = new List<string>();
            List<string> znalezioneMetaDane = new List<string>();

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

                            Pliki wczytanyPlik = _JsonToPlikiMapper.Map(parsedJson);
                            wczytanePliki.Add(dokument, wczytanyPlik);
                        }
                    }
                }
            }

            result.ImportSukces = _Dao.ImportujPliki(wczytanePliki);

            return result;
        }

        public ImportResult ImportKatLokalPapier(bool nadpisz)
        {
            ImportResult result = new ImportResult();
            List<KatLokalPapier> lokalneArchiwum = new List<KatLokalPapier>();
            List<string> filePaths = new List<string>();
            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");

            string sciezkaDoPlikow = Path.Combine(eadRoot, "zet", "KatLokalPapier");

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
                    var parsedArray = rootJson["KatLokalPapier"];

                    foreach (var parsedJson in parsedArray)
                    {
                        KatLokalPapier aktualneArchiwumDoWczytania = _JsonToKatLokalMapper.Map(parsedJson);

                        lokalneArchiwum.Add(aktualneArchiwumDoWczytania);
                    }
                }
            }

            string user = ConfigurationManager.AppSettings["dbuser"];
            string password = ConfigurationManager.AppSettings["dbpassword"];
            string host = ConfigurationManager.AppSettings["dbhost"];
            string port = ConfigurationManager.AppSettings["dbport"];
            string name = ConfigurationManager.AppSettings["dbname"];

            result.ImportSukces = _Dao.ImportujArchiwa(lokalneArchiwum);

            return result;
        }

        public ImportResult ImportFirms(bool nadpisz)
        {
            ImportResult result = new ImportResult();
            List<KatFirmy> lokalneFirmy = new List<KatFirmy>();
            List<string> filePaths = new List<string>();
            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");

            string sciezkaDoPlikow = Path.Combine(eadRoot, "zet", "KatFirmy");

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
                    var parsedArray = rootJson["KatFirmy"];

                    foreach (var parsedJson in parsedArray)
                    {
                        KatFirmy aktualnaFirma = _JsonToKatFirmyMapper.Map(parsedJson);

                        lokalneFirmy.Add(aktualnaFirma);
                    }
                }
            }

            result.ImportSukces = _Dao.ImportujFirmy(lokalneFirmy);

            return result;

        }
        public ImportResult ImportAreas(bool nadpisz)
        {
            ImportResult result = new ImportResult();

            List<KatRejony> lokalneRejony = new List<KatRejony>();
            List<string> filePaths = new List<string>();
            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");

            string sciezkaDoPlikow = Path.Combine(eadRoot, "zet", "KatRejony");

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
                    var parsedArray = rootJson["KatRejony"];

                    foreach (var parsedJson in parsedArray)
                    {
                        KatRejony aktualnyRejon = _JsonToKatRejonyMapper.Map(parsedJson);

                        lokalneRejony.Add(aktualnyRejon);
                    }
                }
            }

            result.ImportSukces = _Dao.ImportujRejony(lokalneRejony);

            return result;
        }

        public ImportResult ImportujPracownikow(string sessionId)
        {
            ImportResult result = new ImportResult();
            string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
            List<Pracownik> pracownicy = new List<Pracownik>();

            string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
            string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatPracownicy");

            string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
            StringBuilder wczytaniPracownicy = new StringBuilder();
            foreach (string plik in pliki)
            {
                using (StreamReader reader = new StreamReader(plik))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        wczytaniPracownicy.Append(line);
                    }
                };

                var parsedJson = JObject.Parse(wczytaniPracownicy.ToString());
                var root = parsedJson["KatPracownicy"];
                foreach (var pracownik in root)
                {
                    Pracownik wczytanyPracownik = _JsonToPracownikMapper.Map(pracownik);

                    pracownicy.Add(wczytanyPracownik);
                }

                result.ImportSukces = _PracownikDao.ImportujPracownikow(pracownicy);
            }
            return result;
        }
    }
}
