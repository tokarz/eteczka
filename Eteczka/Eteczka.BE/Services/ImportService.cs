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
        private IJsonToMiejscePracyMapper _JsonToMiejscePracyMapper;
        private IJsonToPodwydzialMapper _JsonToPodwydzialMapper;
        private IJsonToWydzialMapper _JsonToWydzialMapper;
        private IJsonToKonto5Mapper _JsonToKonto5Mapper;

        private PlikiUtils _PlikiUtils;
        private PlikiDAO _Dao;
        private PracownikDAO _PracownikDao;
        private FirmyDAO _FirmyDao;
        private RejonyDAO _RejonyDao;
        private ArchiwaDAO _ArchiwaDAO;
        private MiejscePracyDAO _MiejscePracyDao;
        private KatPodwydzialDAO _KatPodwydzialDAO;
        private KatWydzialDAO _KatWydzialDAO;
        private Konto5DAO _Konto5DAO;

        public ImportService(
            IJsonToKatLokalMapper mapper,
            IJsonToKatFirmyMapper firmyMapper,
            IJsonToPlikiMapper plikiMapper,
            IJsonToKatRejonyMapper rejonyMapper,
            IJsonToPracownikMapper pracownikMapper,
            IJsonToMiejscePracyMapper jsonToMiejscePracyMapper,
            IJsonToPodwydzialMapper jsonToPodwydzialMapper,
            IJsonToWydzialMapper jsonToWydzialMapper,
            IJsonToKonto5Mapper jsonToKonto5Mapper,
            PlikiUtils plikiUtils,
            PlikiDAO dao,
            PracownikDAO pracownikDao,
            FirmyDAO firmyDao,
            RejonyDAO rejonyDao,
            ArchiwaDAO archiwaDao,
            MiejscePracyDAO miejscePracyDao,
            KatPodwydzialDAO katPodwydzialDAO,
        KatWydzialDAO katWydzialDAO,
        Konto5DAO konto5DAO)
        {
            this._JsonToKatLokalMapper = mapper;
            this._JsonToKatFirmyMapper = firmyMapper;
            this._JsonToPlikiMapper = plikiMapper;
            this._JsonToKatRejonyMapper = rejonyMapper;
            this._JsonToPracownikMapper = pracownikMapper;
            this._JsonToMiejscePracyMapper = jsonToMiejscePracyMapper;
            this._JsonToPodwydzialMapper = jsonToPodwydzialMapper;
            this._JsonToWydzialMapper = jsonToWydzialMapper;
            this._JsonToKonto5Mapper = jsonToKonto5Mapper;
            this._PlikiUtils = plikiUtils;
            this._Dao = dao;
            this._PracownikDao = pracownikDao;
            this._FirmyDao = firmyDao;
            this._RejonyDao = rejonyDao;
            this._ArchiwaDAO = archiwaDao;
            this._MiejscePracyDao = miejscePracyDao;
            this._KatPodwydzialDAO = katPodwydzialDAO;
            this._KatWydzialDAO = katWydzialDAO;
            this._Konto5DAO = konto5DAO;
        }

        public bool DoesFolderExist(string folder)
        {
            bool result = false;
            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");
            string sciezkaDoWaitingRoom = Path.Combine(eadRoot, "waitingroom");
            string finalFolderLocation = Path.Combine(sciezkaDoWaitingRoom, folder);
            return Directory.Exists(finalFolderLocation);
        }

        public bool CreateSourceFolder(string folder)
        {
            bool result = false;
            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");
            string sciezkaDoWaitingRoom = Path.Combine(eadRoot, "waitingroom");
            if (!Directory.Exists(sciezkaDoWaitingRoom))
            {
                Directory.CreateDirectory(sciezkaDoWaitingRoom);
            }
            string finalFolderLocation = Path.Combine(sciezkaDoWaitingRoom, folder);
            if (!Directory.Exists(finalFolderLocation))
            {
                try
                {
                    Directory.CreateDirectory(finalFolderLocation);
                    result = _FirmyDao.ZapiszKatalogRoboczy(folder, finalFolderLocation);
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
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

            result.ImportSukces = _ArchiwaDAO.ImportujArchiwa(lokalneArchiwum);

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

            result.ImportSukces = _FirmyDao.ImportujFirmy(lokalneFirmy);

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

            result.ImportSukces = _RejonyDao.ImportujRejony(lokalneRejony);

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

        public ImportResult ImportWorkplaces(string sessionId)
        {
            ImportResult result = new ImportResult();
            string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
            List<MiejscePracy> miejscaPracy = new List<MiejscePracy>();

            string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
            string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "MiejscePracy");

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
                var root = parsedJson["MiejscePracy"];
                foreach (var miejscePracy in root)
                {
                    MiejscePracy wczytaneMiejscePracy = _JsonToMiejscePracyMapper.Map(miejscePracy);

                    miejscaPracy.Add(wczytaneMiejscePracy);
                }

                result.ImportSukces = _MiejscePracyDao.ImportujMiejscaPracy(miejscaPracy);
            }
            return result;
        }

        public ImportResult ImportSubDepartments(string sessionId)
        {
            ImportResult result = new ImportResult();
            string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
            List<KatPodWydzialy> podwydzialy = new List<KatPodWydzialy>();

            string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
            string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatPodWydzial");

            string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
            StringBuilder wczytanePodwydzialy = new StringBuilder();
            foreach (string plik in pliki)
            {
                using (StreamReader reader = new StreamReader(plik))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        wczytanePodwydzialy.Append(line);
                    }
                };

                var parsedJson = JObject.Parse(wczytanePodwydzialy.ToString());
                var root = parsedJson["KatPodWydzial"];
                foreach (var podwydzial in root)
                {
                    KatPodWydzialy mappedPodwydzial = _JsonToPodwydzialMapper.Map(podwydzial);

                    podwydzialy.Add(mappedPodwydzial);
                }

                result.ImportSukces = _KatPodwydzialDAO.ImportujPodwydzialy(podwydzialy);
            }
            return result;
        }
        public ImportResult ImportDepartments(string sessionId)
        {
            ImportResult result = new ImportResult();
            string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
            List<KatWydzialy> dzialy = new List<KatWydzialy>();

            string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
            string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatWydzial");

            string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
            StringBuilder wczytaneWydzialy = new StringBuilder();
            foreach (string plik in pliki)
            {
                using (StreamReader reader = new StreamReader(plik))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        wczytaneWydzialy.Append(line);
                    }
                };

                var parsedJson = JObject.Parse(wczytaneWydzialy.ToString());
                var root = parsedJson["KatWydzial"];
                foreach (var wydzial in root)
                {
                    KatWydzialy wczytanyWydzial = _JsonToWydzialMapper.Map(wydzial);

                    dzialy.Add(wczytanyWydzial);
                }

                result.ImportSukces = _KatWydzialDAO.ImportujWydzialy(dzialy);
            }
            return result;
        }
        public ImportResult ImportAccounts5(string sessionId)
        {
            ImportResult result = new ImportResult();
            string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
            List<KatKonto5> konta = new List<KatKonto5>();

            string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
            string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatKonta5");

            string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
            StringBuilder wczytaneKonta = new StringBuilder();
            foreach (string plik in pliki)
            {
                using (StreamReader reader = new StreamReader(plik))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        wczytaneKonta.Append(line);
                    }
                };

                var parsedJson = JObject.Parse(wczytaneKonta.ToString());
                var root = parsedJson["KatKonta5"];
                foreach (var konto in root)
                {
                    KatKonto5 wczytaneKonto = _JsonToKonto5Mapper.Map(konto);

                    konta.Add(wczytaneKonto);
                }

                result.ImportSukces = _Konto5DAO.ImportujKonta5(konta);
            }
            return result;
        }


        public ImportResult CheckImportStatus(string type)
        {
            ImportResult result = new ImportResult()
            {
                CountImportDb = 0,
                CountImportJson = 0,
                ImportSukces = false
            };

            switch (type)
            {
                case "users":
                    {
                        string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
                        List<Pracownik> pracownicy = new List<Pracownik>();

                        string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
                        string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatPracownicy");

                        string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
                        StringBuilder wczytaniPracownicy = new StringBuilder();
                        int counter = 0;
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
                                counter += 1;
                            }
                        }

                        result.CountImportJson = counter;
                        result.CountImportDb = _PracownikDao.PoliczPracownikowWBazie();

                        break;
                    }
                case "firms":
                    {
                        string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
                        List<Pracownik> pracownicy = new List<Pracownik>();

                        string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
                        string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatFirmy");

                        string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
                        StringBuilder wczytaniPracownicy = new StringBuilder();
                        int counter = 0;
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
                            var root = parsedJson["KatFirmy"];
                            foreach (var pracownik in root)
                            {
                                counter += 1;
                            }
                        }

                        result.CountImportJson = counter;
                        result.CountImportDb = _FirmyDao.PoliczFirmyWBazie();

                        break;
                    }
                case "areas":
                    {
                        string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
                        List<Pracownik> pracownicy = new List<Pracownik>();

                        string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
                        string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatRejony");

                        string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
                        StringBuilder wczytaniPracownicy = new StringBuilder();
                        int counter = 0;
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
                            var root = parsedJson["KatRejony"];
                            foreach (var pracownik in root)
                            {
                                counter += 1;
                            }
                        }

                        result.CountImportJson = counter;
                        result.CountImportDb = _RejonyDao.PoliczRejonyWBazie();

                        break;
                    }
                case "archives":
                    {
                        string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
                        List<Pracownik> pracownicy = new List<Pracownik>();

                        string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
                        string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatLokalPapier");

                        string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
                        StringBuilder wczytaniPracownicy = new StringBuilder();
                        int counter = 0;
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
                            var root = parsedJson["KatLokalPapier"];
                            foreach (var pracownik in root)
                            {
                                counter += 1;
                            }
                        }

                        result.CountImportJson = counter;
                        result.CountImportDb = _ArchiwaDAO.PoliczArchiwaWBazie();

                        break;
                    }
                case "workplaces":
                    {
                        string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
                        List<Pracownik> pracownicy = new List<Pracownik>();

                        string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
                        string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "MiejscePracy");

                        string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
                        StringBuilder wczytaniPracownicy = new StringBuilder();
                        int counter = 0;
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
                            var root = parsedJson["MiejscePracy"];
                            foreach (var pracownik in root)
                            {
                                counter += 1;
                            }
                        }

                        result.CountImportJson = counter;
                        result.CountImportDb = _MiejscePracyDao.PoliczMiejscaPracyWBazie();

                        break;
                    }
                case "subdepartment":
                    {
                        string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
                        List<Pracownik> pracownicy = new List<Pracownik>();

                        string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
                        string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatPodWydzial");

                        string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
                        StringBuilder wczytaniPracownicy = new StringBuilder();
                        int counter = 0;
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
                            var root = parsedJson["KatPodWydzial"];
                            foreach (var pracownik in root)
                            {
                                counter += 1;
                            }
                        }

                        result.CountImportJson = counter;
                        result.CountImportDb = _KatPodwydzialDAO.PoliczPodwydzialyWBazie();

                        break;
                    }
                case "department":
                    {
                        string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
                        List<Pracownik> pracownicy = new List<Pracownik>();

                        string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
                        string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatWydzial");

                        string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
                        StringBuilder wczytaniPracownicy = new StringBuilder();
                        int counter = 0;
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
                            var root = parsedJson["KatWydzial"];
                            foreach (var pracownik in root)
                            {
                                counter += 1;
                            }
                        }

                        result.CountImportJson = counter;
                        result.CountImportDb = _KatWydzialDAO.PoliczWydzialyWBazie();

                        break;
                    }
                case "account5":
                    {
                        string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
                        List<Pracownik> pracownicy = new List<Pracownik>();

                        string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
                        string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatKonta5");

                        string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
                        StringBuilder wczytaniPracownicy = new StringBuilder();
                        int counter = 0;
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
                            var root = parsedJson["KatKonta5"];
                            foreach (var pracownik in root)
                            {
                                counter += 1;
                            }
                        }

                        result.CountImportJson = counter;
                        result.CountImportDb = _Konto5DAO.PoliczRejonyWBazie();

                        break;
                    }
                default:
                    {
                        break;
                    }

            }

            return result;
        }
    }
}
