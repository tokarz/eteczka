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
    }
}
