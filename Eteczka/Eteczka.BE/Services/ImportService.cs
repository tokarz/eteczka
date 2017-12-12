using System.Collections.Generic;
using Eteczka.DB.DAO;
using Eteczka.DB.Mappers;
using Eteczka.DB.Connection;
using System.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
using Eteczka.BE.DTO;
using System.Text;
using Eteczka.BE.Utils;
using Eteczka.BE.Mappers;
using Eteczka.Model.Entities;
using System;

namespace Eteczka.BE.Services
{
    public class ImportService : IImportService
    {
        private IJsonToKatLokalMapper _JsonToKatLokalMapper;
        private IJsonToKatFirmyMapper _JsonToKatFirmyMapper;
        private IJsonToKatRejonyMapper _JsonToKatRejonyMapper;
        private IJsonToPracownikMapper _JsonToPracownikMapper;
        private IJsonToMiejscePracyMapper _JsonToMiejscePracyMapper;
        private IJsonToPodwydzialMapper _JsonToPodwydzialMapper;
        private IJsonToWydzialMapper _JsonToWydzialMapper;
        private IJsonToKonto5Mapper _JsonToKonto5Mapper;
        private IJsonToKatDokumentyRodzajMapper _JsonToKatDokumentyRodzajMapper;
        private IKatRodzajeDokumentowExcelMapper _KatRodzajeDokumentowExcelMapper;

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
        private KatDokumentyRodzajDAO _KatDokumentyRodzajDAO;

        public ImportService(
            IJsonToKatLokalMapper mapper,
            IJsonToKatFirmyMapper firmyMapper,
            IJsonToKatRejonyMapper rejonyMapper,
            IJsonToPracownikMapper pracownikMapper,
            IJsonToMiejscePracyMapper jsonToMiejscePracyMapper,
            IJsonToPodwydzialMapper jsonToPodwydzialMapper,
            IJsonToWydzialMapper jsonToWydzialMapper,
            IJsonToKonto5Mapper jsonToKonto5Mapper,
            IJsonToKatDokumentyRodzajMapper jsonToKatDokumentyRodzajMapper,
            IKatRodzajeDokumentowExcelMapper katRodzajeDokumentowExcelMapper,
            PlikiUtils plikiUtils,
            PlikiDAO dao,
            PracownikDAO pracownikDao,
            FirmyDAO firmyDao,
            RejonyDAO rejonyDao,
            ArchiwaDAO archiwaDao,
            MiejscePracyDAO miejscePracyDao,
            KatPodwydzialDAO katPodwydzialDAO,
        KatWydzialDAO katWydzialDAO,
        Konto5DAO konto5DAO, KatDokumentyRodzajDAO KatDokumentyRodzajDAO)
        {
            this._JsonToKatLokalMapper = mapper;
            this._JsonToKatFirmyMapper = firmyMapper;
            this._JsonToKatRejonyMapper = rejonyMapper;
            this._JsonToPracownikMapper = pracownikMapper;
            this._JsonToMiejscePracyMapper = jsonToMiejscePracyMapper;
            this._JsonToPodwydzialMapper = jsonToPodwydzialMapper;
            this._JsonToWydzialMapper = jsonToWydzialMapper;
            this._JsonToKonto5Mapper = jsonToKonto5Mapper;
            this._JsonToKatDokumentyRodzajMapper = jsonToKatDokumentyRodzajMapper;
            this._KatRodzajeDokumentowExcelMapper = katRodzajeDokumentowExcelMapper;
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
            this._KatDokumentyRodzajDAO = KatDokumentyRodzajDAO;
        }

        public bool DoesFolderExist(string folder)
        {
            bool result = false;
            string eadRoot = System.Environment.GetEnvironmentVariable("EAD_DIR");
            string sciezkaDoWaitingRoom = Path.Combine(eadRoot, "waitingroom");
            if (!String.IsNullOrEmpty(folder))
            {
                string finalFolderLocation = Path.Combine(sciezkaDoWaitingRoom, folder.Trim());
                result = Directory.Exists(finalFolderLocation);
            }
            return result;
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
            if (eadRoot == null)
            {
                throw new Exception("EAD_DIR_NOT_SET!");
            }

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

        public ImportResult ImportKatDokumentyRodzaj(string sessionId)
        {
            ImportResult result = new ImportResult();
            string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
            List<KatDokumentyRodzaj> rodzajeDokumentow = new List<KatDokumentyRodzaj>();

            string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
            string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatDokumentyRodzaj");

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
                var root = parsedJson["KatDokumentyRodzaj"];
                foreach (var rodzajDok in root)
                {
                    KatDokumentyRodzaj wczytanyRodzaj = _JsonToKatDokumentyRodzajMapper.Map(rodzajDok);

                    rodzajeDokumentow.Add(wczytanyRodzaj);
                }

                result.ImportSukces = _KatDokumentyRodzajDAO.ImportujRodzajeDokumentow(rodzajeDokumentow);
            }
            return result;
        }
        

        public ImportResult WczytajDokZExcela(bool nadpisz = true)
        {
            ImportResult result = new ImportResult();
            string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");

            string sciezkaDoPliku = Path.Combine(eadRoot, "excel\\Rodzaje_dokumentow_Eteczka.xlsx");

            result.ImportSukces = _KatDokumentyRodzajDAO.ZapiszRodzajeDokDoBazy(sciezkaDoPliku);

            return result;
        }
    }
}
