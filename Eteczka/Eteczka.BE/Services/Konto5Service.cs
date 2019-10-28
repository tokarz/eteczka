using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Services
{
    public class Konto5Service : IKonto5Service
    {

        private Konto5DAO _konto5DAO;

        public Konto5Service(Konto5DAO konto5DAO)
        {
            this._konto5DAO = konto5DAO;
        }
        public List<KatKonto5> PobierzKonta5(string firma)
        {
            List<KatKonto5> pobraneKonta5 = _konto5DAO.pobierajKonto5DlaFirmy(firma);

            return pobraneKonta5;
        }

        public InsertResult DodajKonto5(KatKonto5 konto, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();

            if (!_konto5DAO.SprawdzCzyKonto5IstniejeWFirmie(konto.Firma, konto.Konto5))
            {
                result.Result = _konto5DAO.DodajKonto5(konto, idoper, idakcept);
                result.Message = result.Result == true ? "Konto5 zostało dodane." : "Konto5 nie zostało dodane.";
            }
            else
            {
                result.Message = "Takie konto5 już istnieje. Pozycja nie została dodana.";
            }

            return result;
        }

        public InsertResult EdytujKonto5(KatKonto5 konto, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();

            if (_konto5DAO.SprawdzCzyKonto5IstniejeWFirmie(konto.Firma, konto.Konto5))
            {
                result.Result = _konto5DAO.EdytujKonto5(konto, idoper, idakcept);
                result.Message = result.Result == true ? "Zapisano zmiany." : "Zmiany nie zostały zapisane.";
            }
            else
            {
                result.Message = "Edycja nie powiodła się. Podane konto5 nie istnieje.";
            }

            return result;
        }

        public InsertResult UsunKonto5(KatKonto5 konto, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();

            if (_konto5DAO.SprawdzCzyKonto5IstniejeWFirmie(konto.Firma, konto.Konto5))
            {
                result.Result = _konto5DAO.UsunKonto5(konto.Firma, konto.Konto5, idoper, idakcept);
                result.Message = result.Result == true ? "Konto5 zostało przeniesione na listę nieaktywnych." : "Usuwanie nie powiodło się.";
            }
            else
            {
                result.Message = "Usuwanie nie powiodło się. Wskazane konto 5 nie istnieje.";
            }

            return result;
        }

        public List<KatKonto5> WyszukajKonto5(string firma, string search)
        {
            List<KatKonto5> result = _konto5DAO.WyszukajKonto5(firma, search);

            return result;
        }



    }
}
