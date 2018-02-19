using Eteczka.BE.DTO;
using Eteczka.BE.Utils;
using Eteczka.DB.DAO;
using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Services
{
    public class ImportStateService : IImportStateService
    {
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


        public ImportStateService(PlikiUtils plikiUtils,
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
                        string eadRoot = ConfigurationManager.AppSettings["rootdir"];
                        List<Pracownik> pracownicy = new List<Pracownik>();

                        if (eadRoot == null)
                        {
                            throw new Exception("EAD_DIR_NOT_SET!");
                        }
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
                        string eadRoot = ConfigurationManager.AppSettings["rootdir"];
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
                        string eadRoot = ConfigurationManager.AppSettings["rootdir"];
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
                        string eadRoot = ConfigurationManager.AppSettings["rootdir"];
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
                        string eadRoot = ConfigurationManager.AppSettings["rootdir"];
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
                        string eadRoot = ConfigurationManager.AppSettings["rootdir"];
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
                        string eadRoot = ConfigurationManager.AppSettings["rootdir"];
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
                        string eadRoot = ConfigurationManager.AppSettings["rootdir"];
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
                case "dokRodzaj":
                    {
                        string eadRoot = ConfigurationManager.AppSettings["rootdir"];
                        List<Pracownik> pracownicy = new List<Pracownik>();

                        string sciezkaDoKatalogu = Path.Combine(eadRoot, "zet");
                        string sciezkaDoPlikow = Path.Combine(sciezkaDoKatalogu, "KatDokumentyRodzaj");

                        string[] pliki = Directory.GetFiles(sciezkaDoPlikow);
                        StringBuilder wczytaneDokumentyRodzaj = new StringBuilder();
                        int counter = 0;
                        foreach (string plik in pliki)
                        {
                            using (StreamReader reader = new StreamReader(plik))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    wczytaneDokumentyRodzaj.Append(line);
                                }
                            };

                            var parsedJson = JObject.Parse(wczytaneDokumentyRodzaj.ToString());
                            var root = parsedJson["KatDokumentyRodzaj"];
                            foreach (var pracownik in root)
                            {
                                counter += 1;
                            }
                        }

                        result.CountImportJson = counter;
                        result.CountImportDb = _KatDokumentyRodzajDAO.PoliczRodzajeWBazie();

                        //string eadRoot = ConfigurationManager.AppSettings["rootdir"];
                        //List<Pracownik> pracownicy = new List<Pracownik>();

                        //string sciezkaDoKatalogu = Path.Combine(eadRoot, "excel");
                        //string plik = Path.Combine(sciezkaDoKatalogu, "Rodzaje_dokumentow_Eteczka.xlsx");

                        //List<KatDokumentyRodzaj> rodzajeDokumentow = _KatRodzajeDokumentowExcelMapper.PobierzRodzajeDokZExcela(plik);

                        //result.CountImportJson = rodzajeDokumentow.Count;
                        //result.CountImportDb = _KatDokumentyRodzajDAO.PoliczRodzajeWBazie();

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
