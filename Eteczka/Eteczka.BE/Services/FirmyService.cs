using Eteczka.DB.DAO;
using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public class FirmyService : IFirmyService
    {
        private IFirmyDAO _Dao;
        public FirmyService(IFirmyDAO firmaDAO)
        {
            this._Dao = firmaDAO;
        }

        public List<KatFirmy> PobierzWszystkie()
        {
            List<KatFirmy> PobraneFirmy = _Dao.PobierzFirmyZBazy();
            return PobraneFirmy;
        }

        public List<KatFirmy>PobierzWszystkieAktywneFirmy()
        {

            List<KatFirmy> PobraneFirmy = _Dao.PobierzFirmyZBazy().FindAll(x => x.Usuniety == false);

            return PobraneFirmy;
            
        }

        public List<KatFirmy> PobierzWszystkieNieaktywneFirmy()
        {
            List<KatFirmy> PobraneFirmy = _Dao.PobierzFirmyZBazy().FindAll(x => x.Usuniety == true);

            return PobraneFirmy;

        }

        public KatFirmy WyszukajFirmePoNipie(string nip)
        {
            KatFirmy wyszukanaFirma = _Dao.WyszukajFirmePoNipie(nip);

            return wyszukanaFirma;
        }

        public InsertResult DodajFirme(KatFirmy firmaDoDodania, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();
            if (_Dao.WyszukajFirmePoNipie(firmaDoDodania.Nip) == null)
            {

                result.Result = _Dao.DodajFirme(firmaDoDodania, idoper, idakcept);
                result.Message = result.Result ? "Firma została dodana" : "Próba dodania firmy nie powiodła się.";
            }
            else
            {
                result.Message = "Próba dodania firmy nie powiodła się. Firma o podanym numerze NIP istnieje w bazie.";
            }
            
            return result;
        }

        public InsertResult EdytujFirme(KatFirmy firmaDoEdycji, string idoper, string idakcept)
        {

            InsertResult result = new InsertResult();

            result.Result = _Dao.EdytujFirme(firmaDoEdycji, idoper, idakcept);
            result.Message = result.Result == true ? "Zapisano zmiany." : "Próba edycji nie powiodła się.";
           
            return result;
        }

        public InsertResult UsunFirme(string nip)
        {

            InsertResult result = new InsertResult();

            result.Result = _Dao.UsunFirme(nip);
            result.Message = result.Result == true ? "Firma została usunięta." : "Próba usunięcia firmy nie powiodła się.";

            return result;
        }
    }
}
