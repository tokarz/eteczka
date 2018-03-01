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

        public bool ZmienHaslaPlikow(string stareHaslo, string noweHaslo)
        {

            List<string> plikiDoZmianyHasla = new List<string>();
            List<string> firmyZPlikami = new List<string>();
            List<Pliki> pobrane = _Dao.PobierzWszystkiePliki("asc", "datapocz");

            string eadRootName = ConfigurationManager.AppSettings["rootdir"];
            string filesFolder = ConfigurationManager.AppSettings["filesdir"];
            string EAD_ROOT = ConfigurationManager.AppSettings["rootdir"];



            foreach (Pliki plik in pobrane)
            {
                if (!firmyZPlikami.Contains(plik.Firma))
                {
                    firmyZPlikami.Add(plik.Firma);
                }
            }

            foreach (string firma in firmyZPlikami)
            {
                string sciezkaFolderu = Path.Combine(EAD_ROOT, filesFolder, firma);
                List<string> plikiFirmy = Directory.GetFiles(sciezkaFolderu, "*.pdf").ToList();
                plikiDoZmianyHasla.AddRange(plikiFirmy);
            }

            return _PlikiUtils.ZmienHasloPlikow(plikiDoZmianyHasla, stareHaslo, noweHaslo);
        }

        public List<Pliki> PobierzWszystkie(string sortOrder = "asc", string sortColumn = "datapocz")
        {

            List<Pliki> pobrane = _Dao.PobierzWszystkiePliki(sortOrder, sortColumn);

            return pobrane;
        }

        public List<Pliki> PobierzDlaUzytkownika(string numeread, string firma)
        {
            List<Pliki> result = _Dao.PobierzPlikPoNumerzeEad(numeread, firma);

            return result;
        }

        public List<Pliki> PobierzZawierajaceTekst(string searchText, string sortOrder = "asc", string sortColumn = "datapocz")
        {
            List<Pliki> result = new List<Pliki>();

            return result;
        }

        public bool ZakomitujPlikDoBazy(KomitPliku plik, string firma, string waitingRoom, string idOper)
        {
            bool result = false;

            string nazwaPliku = firma.Trim() + "_" + DateTime.Now.Millisecond + "_" + plik.Nazwa.Trim();

            string eadRoot = ConfigurationManager.AppSettings["rootdir"];
            string katalogZrodlowy = Path.Combine(eadRoot, "waitingroom", firma.Trim(), waitingRoom);
            string plikZrodlowy = Path.Combine(katalogZrodlowy, plik.Nazwa.Trim());

            string katalogDocelowy = Path.Combine(eadRoot, "pliki", firma.Trim());

            result = _Dao.KomitujPlikDoBazy(plik, plik.Nazwa.Trim(), nazwaPliku, katalogDocelowy, plikZrodlowy, firma, idOper);
            if (result == true)
            {
                if (!Directory.Exists(katalogDocelowy))
                {
                    Directory.CreateDirectory(katalogDocelowy);
                }
                string plikDocelowy = Path.Combine(katalogDocelowy, nazwaPliku);
                if (File.Exists(plikZrodlowy) && !File.Exists(plikDocelowy))
                {
                    File.Move(plikZrodlowy, plikDocelowy);
                    if (File.Exists(plikDocelowy))
                    {
                        _PlikiUtils.ZaszyfrujIPrzeniesPlikPdf(plikDocelowy);
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            //Transakcja:
            //1) Zrob insert
            //2)Skopiuj plik
            //3) Weryfikuj

            return result;
        }



        public List<string> PobierzFolderyDlaFirmy(string firma, string folder, string sortOrder = "asc", string sortColumn = "datapocz")
        {
            List<string> result = new List<string>();
            string eadRoot = ConfigurationManager.AppSettings["rootdir"];
            if (folder != null)
            {
                string sciezkaDoWaitingRoomu = Path.Combine(eadRoot, "waitingroom", firma, folder);

                if (Directory.Exists(sciezkaDoWaitingRoomu))
                {
                    string[] foldery = Directory.GetDirectories(sciezkaDoWaitingRoomu);
                    
                    foreach (string pelnaSciezkaDoFolderu in foldery)
                    {
                        string samFolder = _PlikiUtils.WezNazweFolderuZeSciezki(pelnaSciezkaDoFolderu);

                        result.Add(samFolder);
                    }
                }
            }

            return result;
        }

        public List<Pliki> PobierzPlikiDlaFirmy(string firma, string folder, string sortOrder = "asc", string sortColumn = "datapocz")
        {
            List<Pliki> result = new List<Pliki>();
            string eadRoot = ConfigurationManager.AppSettings["rootdir"];
            if (folder != null)
            {
                string sciezkaDoWaitingRoomu = Path.Combine(eadRoot, "waitingroom", firma, folder);

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

            string eadRoot = ConfigurationManager.AppSettings["rootdir"];


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

        public List<Pliki> SzukajPlikiZFiltrow(SessionDetails sesja, FiltryPlikow filtry, string sortOrder, string sortColumn)
        {
            List<Pliki> wyszukanePliki = new List<Pliki>();
            if (filtry != null)
            {

                string rejon = (filtry.Rejon != null) ? ("%" + filtry.Rejon.Rejon + "%") : "%%";
                string wydzial = (filtry.Wydzial != null) ? ("%" + filtry.Wydzial.Wydzial + "%") : "%%";
                string podwydzial = (filtry.Podwydzial != null) ? ("%" + filtry.Podwydzial.Podwydzial + "%") : "%%";
                string konto5 = (filtry.Konto5 != null) ? ("%" + filtry.Konto5.Konto5 + "%") : "%%";
                string typ = (filtry.Typ != null) ? ("%" + filtry.Typ.SymbolEad + "%") : "%%";

                wyszukanePliki = _Dao.WyszukajPlikiZFiltrow(sesja.AktywnaFirma.Firma, rejon, wydzial, podwydzial, konto5, typ, filtry.Pracownik, sortOrder, sortColumn);
            }
            return wyszukanePliki;
        }

        public bool WyslijPlikiMailem(SessionDetails sesja, string adresaci, List<string> Zalaczniki, string hasloDoZip, string temat, string wiadomosc)
        {

            bool result = _PlikiUtils.WyslijPlikiMailem(sesja.AktywnaFirma.Firma, adresaci, Zalaczniki, hasloDoZip, temat, wiadomosc);

            return result;
        }

        public bool EdytujDokumentWBazie(SessionDetails sesja, KomitPliku plik, string idPliku)
        {
            bool result = _Dao.EdytujPlikWBazie(plik, sesja.AktywnaFirma.Identyfikator, sesja.AktywnaFirma.Identyfikator, idPliku);

            return result;
        }
        public bool UsunDokumentWBazie(SessionDetails sesja, KomitPliku plik, string idPliku)
        {
            bool result = _Dao.UsunDokumentZBazy(plik, sesja.AktywnaFirma.Identyfikator, sesja.AktywnaFirma.Identyfikator, idPliku);
            return result;
        }

        public List<Pliki> SzukajOstatnioDodanePlikiPrac(SessionDetails sesja, string numeread, int liczbaPlikow)
        {
            List<Pliki> WyszukanePliki = _Dao.ZnajdzOstatnioDodanePlikiPracownika(numeread, sesja.AktywnaFirma.Firma, liczbaPlikow);

            return WyszukanePliki;
        }
        public int ZliczPlikiWTeczcePracownika(SessionDetails sesja, string numeread)
        {
            int result = _Dao.PoliczPlikiPracownikaWTeczce(numeread, sesja.AktywnaFirma.Firma);

            return result;
        }




    }
}
