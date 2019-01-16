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
            KatFirmy firmaWBazie = _Dao.WyszukajFirmePoNipie(firmaDoDodania.Nip);
            if (firmaWBazie == null)
            {

                result.Result = _Dao.DodajFirme(firmaDoDodania, idoper, idakcept);
                result.Message = result.Result ? "Firma została dodana" : "Próba dodania firmy nie powiodła się.";
            }

            else if (firmaWBazie != null && firmaWBazie.Usuniety == true )
            {
                result.Result = _Dao.PrzywrocFirmeZBazy(firmaDoDodania.Nip);
                result.Message = result.Result ? "Firma o podanym numerze NIP znajdowała się już w bazie. Zmieniono status firmy na aktywny." : "Próba dodania firmy nie powiodła się.";

            }
            else
            {
                result.Message = "Próba dodania firmy nie powiodła się. Firma o podanym numerze NIP istnieje w bazie.";
            }
            
            return result;
        }

        public InsertResult EdytujFirme(KatFirmy firmaDoEdycji, string nip,string idoper, string idakcept)
        {

            InsertResult result = new InsertResult();

            if (firmaDoEdycji.Nip == nip)
            {
                if (_Dao.WyszukajFirmePoNipie(nip) != null)
                {
                    result.Result = _Dao.EdytujFirme(firmaDoEdycji, idoper, idakcept);
                    result.Message = result.Result == true ? "Zapisano zmiany." : "Próba edycji nie powiodła się.";
                }
                else
                {
                    result.Message = "Próba edycji firmy nie powiodła się.";
                }
            }
            else
            {
                if (_Dao.WyszukajFirmePoNipie(nip) != null)
                {
                    if (_Dao.WyszukajFirmePoNipie(firmaDoEdycji.Nip) == null)
                    {
                        result.Result = _Dao.EdytujFirme(firmaDoEdycji, idoper, idakcept);
                        result.Message = result.Result == true ? "Zapisano zmiany." : "Próba edycji nie powiodła się.";
                    }

                    else
                    {
                        result.Message = "Edycja nie powiodła się. Podany numer NIP już figuruje w bazie.";
                    }
                }
                else
                {
                    result.Message = "Próba edycji firmy nie powiodła się.";
                }
            }       
            return result;
        }

        public InsertResult UsunFirme(string nip)
        {

            InsertResult result = new InsertResult();

            if (_Dao.WyszukajFirmePoNipie(nip) != null)
            {
                result.Result = _Dao.DezaktywujFirme(nip);
                result.Message = result.Result == true ? "Firma została usunięta." : "Próba usunięcia firmy nie powiodła się.";
            }
            else
            {
                result.Message = "Próba usunięcia firmy nie powiodła się.";
            }
                

            return result;
        }
    }
}
