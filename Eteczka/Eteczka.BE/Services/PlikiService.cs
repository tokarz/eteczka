using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.DB.DAO;
using Eteczka.DB.Connection;
using Eteczka.DB.Entities;
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

        public List<KatTeczki> PobierzWszystkie(string sortOrder = "asc", string sortColumn = "Id")
        {

            List<KatTeczki> pobrane = _Dao.PobierzWszystkiePliki(sortOrder, sortColumn);

            return pobrane;
        }

        public List<KatTeczki> PobierzDlaUzytkownika(string userId, string sortOdred = "asc", string sortColumn = "Id")
        {
            List<KatTeczki> result = new List<KatTeczki>();

            return result;
        }

        public List<KatTeczki> PobierzZawierajaceTekst(string searchText, string sortOrder = "asc", string sortColumn = "Id")
        {
            List<KatTeczki> result = new List<KatTeczki>();

            return result;
        }

        public MetaDanePliku PobierzMetadane(string plik)
        {
            MetaDanePliku result = new MetaDanePliku();

            KatTeczki pobranyPlik = _Dao.PobierzPlikPoNazwie(plik);

            result.ModificationDate = pobranyPlik.DataModyfikacji;
            result.Jrwa = pobranyPlik.Jrwa;
            result.Type = pobranyPlik.TypDokumentu;

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

        public StanPlikow PobierzStanPlikow(StanSesji sesja)
        {
            StanPlikow result = new StanPlikow();
            result.PlikiPozaSystemem = new List<string>();
            result.PlikiWSystemie = new List<MetaDanePliku>();
            List<string> plikiSkanera = new List<string>();
            if (sesja != null)
            {
                string katalogPlikow = Path.Combine(ConfigurationManager.AppSettings["rootdir"], "pliki");
                if (Directory.Exists(katalogPlikow))
                {
                    string[] plikiZeskanowane = Directory.GetFiles(katalogPlikow);
                    foreach (string zeskanowanyPlik in plikiZeskanowane)
                    {
                        string nazwaPliku = _PlikiUtils.WezNazwePlikuZeSciezki(zeskanowanyPlik);
                        if(File.Exists(nazwaPliku + ".json"))
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
            }

            return result;
        }

        
    }
}
