using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using Eteczka.Model.DTO;
using System.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
using Eteczka.BE.Model;
using Eteczka.BE.Utils;

namespace Eteczka.BE.Services
{
    public class PlikiService : IPlikiService
    {
        private PlikiDAO _Dao;
        private PlikiUtils _PlikiUtils;

        public PlikiService(PlikiDAO dao, PlikiUtils plikiUtils)
        {
            this._Dao = dao;
            this._PlikiUtils = plikiUtils;
        }

        public List<Pliki> PobierzWszystkie(string sortOrder = "asc", string sortColumn = "Id")
        {

            List<Pliki> pobrane = _Dao.PobierzWszystkiePliki(sortOrder, sortColumn);

            return pobrane;
        }

        public List<Pliki> PobierzDlaUzytkownika(string numeread, string sortOdred = "asc", string sortColumn = "Id")
        {
            List<Pliki> result = _Dao.PobierzPlikPoNumerzeEad(numeread);

            return result;
        }

        public List<Pliki> PobierzZawierajaceTekst(string searchText, string sortOrder = "asc", string sortColumn = "Id")
        {
            List<Pliki> result = new List<Pliki>();

            return result;
        }

        public bool ZakomitujPlikDoBazy(KomitPliku plik, string firma, string idOper)
        {
            bool result = false;

            result = _Dao.KomitujPlik(plik, firma, idOper);

            //Transakcja:
            //1) Zrob insert
            //2)Skopiuj plik
            //3) Weryfikuj

            return result;
        }

        public List<Pliki> PobierzPlikiDlaFirmy(string firma)
        {
            List<Pliki> result = new List<Pliki>();
            string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");
            string sciezkaDoWaitingRoomu = Path.Combine(eadRoot, "waitingroom", firma);

            if (Directory.Exists(sciezkaDoWaitingRoomu))
            {
                string[] plikiDlaFirmy = Directory.GetFiles(sciezkaDoWaitingRoomu);
                foreach (string plikDlaFirmy in plikiDlaFirmy)
                {

                    Pliki plik = new Pliki()
                    {
                        NazwaScan = _PlikiUtils.WezNazwePlikuZeSciezki(plikDlaFirmy),
                        NazwaEad = _PlikiUtils.WezNazwePlikuZeSciezki(plikDlaFirmy),
                        DataDokumentu = File.GetCreationTime(plikDlaFirmy),
                        PelnasciezkaEad = plikDlaFirmy
                    };

                    result.Add(plik);
                }

            }

            return result;
        }

        public MetaDanePliku PobierzMetadane(string plik)
        {
            MetaDanePliku result = new MetaDanePliku();

            Pliki pobranyPlik = _Dao.PobierzPlikPoNazwie(plik);

            result.ModificationDate = pobranyPlik.DataModyfikacji;
            result.Jrwa = pobranyPlik.NumerEad;
            result.Type = "" + pobranyPlik.TypPliku;

            string eadRoot = Environment.GetEnvironmentVariable("EAD_DIR");


            string sciezkaDoPlikow = Path.Combine(eadRoot, "pliki");
            string plikDoImportu = Path.Combine(sciezkaDoPlikow, plik);

            if (File.Exists(plikDoImportu))
            {
                result.CreationDate = File.GetCreationTime(plikDoImportu);
                result.Size = new FileInfo(plikDoImportu).Length + "";
            }

            result.PhysicalLocation = "??";

            return result;
        }

        public StanPlikow PobierzStanPlikow(string sessionId)
        {
            StanPlikow result = new StanPlikow();
            result.PlikiPozaSystemem = new List<string>();
            result.PlikiWSystemie = new List<MetaDanePliku>();
            List<string> plikiSkanera = new List<string>();

            string katalogPlikow = Path.Combine(ConfigurationManager.AppSettings["rootdir"], "pliki");
            if (Directory.Exists(katalogPlikow))
            {
                string[] plikiZeskanowane = Directory.GetFiles(katalogPlikow);
                foreach (string zeskanowanyPlik in plikiZeskanowane)
                {
                    string nazwaPliku = _PlikiUtils.WezNazwePlikuZeSciezki(zeskanowanyPlik);
                    if (File.Exists(nazwaPliku + ".json"))
                    {
                        MetaDanePliku danePliku = PobierzMetadane(zeskanowanyPlik);
                        result.PlikiWSystemie.Add(danePliku);
                    }
                    else
                    {
                        result.PlikiPozaSystemem.Add(nazwaPliku);
                    }
                }
            }

            return result;
        }


    }
}
