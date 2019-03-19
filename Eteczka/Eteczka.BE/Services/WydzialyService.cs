using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.DB.DAO;
using Eteczka.Model.Entities;
using Eteczka.BE.Mappers;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Services
{
    public class WydzialyService : IWydzialyService
    {
        private KatWydzialDAO _WydzialDao;

        public WydzialyService(KatWydzialDAO wydzialDao)
        {
            this._WydzialDao = wydzialDao;
        }

        public List<KatWydzialy> PobierzWydzialyDlaFirmy(SessionDetails sesja)
        {
            List<KatWydzialy> pobraneWydzialy = _WydzialDao.PobierzDlaFirmy(sesja.AktywnaFirma.Firma);

            return pobraneWydzialy;
        }

        public InsertResult DodajWydzialDlaFirmy(KatWydzialy wydzialDoDodania, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();
            if (_WydzialDao.SprawdzCzyWydzialIstniejeWFirmie(wydzialDoDodania.Firma, wydzialDoDodania.Wydzial))
            {
                result.Result = false;
                result.Message = "Dodawanie nie powiodło się. W tej firmie już istnieje taki wydział.";
            }
            else
            {
                result.Result = _WydzialDao.DodajWydzialDlaFirmy(wydzialDoDodania, idoper, idakcept);
                result.Message = result.Result == true ? "Wydział został dodany." : "Dodawanie wydziału nie powiodło się.";
            }

            return result;
        }

        public InsertResult EdytujWydzialDlaFirmy(KatWydzialy wydzialDoEdycji, string idoper, string idakcept)
        {

            InsertResult result = new InsertResult();

            if (_WydzialDao.SprawdzCzyWydzialIstniejeWFirmie(wydzialDoEdycji.Firma, wydzialDoEdycji.Wydzial))
            {
                result.Result = _WydzialDao.EdytujWydzialDlaFirmy(wydzialDoEdycji, idoper, idakcept);
                result.Message = result.Result == true ? "Edycja zakończona pomyślnie." : "Próba edycji nie powiodła się.";
            }
            else
            {
                result.Result = false;
                result.Message = "Edycja nie powiodła się. W tej firmie nie istnieje taki wydział.";
            }

                return result;
        }

        public InsertResult UsunWydzialZFirmy(KatWydzialy wydzialDoUsuniecia, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();

            if (_WydzialDao.SprawdzCzyWydzialIstniejeWFirmie(wydzialDoUsuniecia.Firma, wydzialDoUsuniecia.Wydzial))
            {
                result.Result = _WydzialDao.UsunWydzialZFirmy(wydzialDoUsuniecia.Firma, wydzialDoUsuniecia.Wydzial, idoper, idakcept);
                result.Message = result.Result == true ? "Wydział został przeniesiony na listę nieaktywnych." : " Usuwanie wydziału nie powiodło się";
            }
            else
            {
                result.Result = false;
                result.Message = "Usuwanie nie powiodło się. Podany wydział nie istnieje.";
            }

            return result;
        }
    }
}
