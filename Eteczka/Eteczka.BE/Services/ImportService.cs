﻿using System;
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
        private PlikiUtils _PlikiUtils;
        private PlikiDAO _Dao;

        public ImportService(PlikiUtils plikiUtils)
        {
            this._PlikiUtils = plikiUtils;
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
                        Pliki wczytanyPlik = new Pliki();
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
                            wczytanyPlik.DataAkcept = DateTime.Parse(parsedJson["pesel"].ToString());
                            wczytanyPlik.DataDokumentu = DateTime.Parse(parsedJson["datadokumentu"].ToString());
                            wczytanyPlik.DataModyfikacji = DateTime.Parse(parsedJson["datamodyfikacji"].ToString());
                            wczytanyPlik.DataPocz = DateTime.Parse(parsedJson["datapocz"].ToString());
                            wczytanyPlik.DataSkanu = DateTime.Parse(parsedJson["dataSkanu"].ToString());
                            wczytanyPlik.DokumentWlasny = bool.Parse(parsedJson["dokumentwlasny"].ToString());
                            wczytanyPlik.IdAkcept = parsedJson["idakcept"].ToString();
                            wczytanyPlik.IdOper = parsedJson["idoper"].ToString();
                            wczytanyPlik.NazwaPliku = parsedJson["nazwapliku"].ToString();
                            wczytanyPlik.NumerEad = parsedJson["numeread"].ToString();
                            wczytanyPlik.OpisDodatkowy = parsedJson["opisdodatkowy"].ToString();
                            wczytanyPlik.PelnaSciezka = parsedJson["pelnasciezka"].ToString();
                            wczytanyPlik.Symbol = parsedJson["symbol"].ToString();
                            wczytanyPlik.TypPliku = parsedJson["typpliku"].ToString();
                        }

                        wczytanePliki.Add(dokument, wczytanyPlik);
                    }
                }
            }

            result.ImportSukces = _Dao.ImportujPliki(wczytanePliki);

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
                        aktualneArchiwumDoWczytania.Firma = parsedJson["firma"].ToString();
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

            result.ImportSukces = _Dao.ImportujArchiwa(lokalneArchiwum);

            return result;
        }

        public ImportResult ImportFirms(bool nadpisz)
        {
            ImportResult result = new ImportResult();
            List<KatFirmy> lokalneFirmy = new List<KatFirmy>();
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
                        KatFirmy aktualnaFirma = new KatFirmy();

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

            result.ImportSukces = _Dao.ImportujFirmy(lokalneFirmy);

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

                        aktualnyRejon.Rejon = parsedJson["rejon"].ToString();
                        aktualnyRejon.Nazwa = parsedJson["nazwa"].ToString();
                        aktualnyRejon.Firma = parsedJson["firma"].ToString();
                        aktualnyRejon.Mnemonik = parsedJson["mnemonik"].ToString();
                        aktualnyRejon.Datamodify = DateTime.Now;
                        aktualnyRejon.Idoper = 0;
                        aktualnyRejon.Idakcept = 0;
                        aktualnyRejon.Dataakcept = DateTime.Now;

                        lokalneRejony.Add(aktualnyRejon);
                    }
                }
            }

            result.ImportSukces = _Dao.ImportujRejony(lokalneRejony);

            return result;
        }
    }
}
