using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;
using Eteczka.BE.Mappers;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Services
{
    public class RejonyService : IRejonyService
    {
        private IRejonyDAO _RejonDao;

        public RejonyService(IRejonyDAO rejonDao)
        {
            this._RejonDao = rejonDao;
        }

        public List<KatRejony> PobierzRejony()
        {
            List<KatRejony> pobraneRejony = _RejonDao.PobieraczRejonow();

            return pobraneRejony;
        }
        public List<KatRejony> PobierzRejonyDlaFirmy(SessionDetails sesja)
        {
            List<KatRejony> pobraneRejony = _RejonDao.PobieraczRejonowDlaFirmy(sesja.AktywnaFirma.Firma);
            return pobraneRejony;
        }

        public InsertResult DodajRejonDlaFirmy(KatRejony rejonDoDodania, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();
            if (_RejonDao.SprawdzCzyRejonIstniejeWFirmie(rejonDoDodania.Rejon, rejonDoDodania.Firma))
            {
                result.Result = false;
                result.Message = "Dodawanie nie powiodło się. W tej firmie już istnieje taki rejon.";
            }
            else
            {
                result.Result = _RejonDao.DodajRejonDlaFirmy(rejonDoDodania, idoper, idakcept);
                result.Message = result.Result == true ? "Rejon został dodany." : "Dodawanie rejonu nie powiodło się.";
            }

            return result;

        }
        public InsertResult EdytujRejonDlaFirmy(KatRejony rejonDoEdycji, string rejonPrzedZmiana, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();


            if (rejonDoEdycji.Rejon == rejonPrzedZmiana)
            {
                if (_RejonDao.SprawdzCzyRejonIstniejeWFirmie(rejonPrzedZmiana, rejonDoEdycji.Firma))
                {
                    result.Result = _RejonDao.EdytujRejonDlaFirmy(rejonDoEdycji, rejonPrzedZmiana, idoper, idakcept);
                    result.Message = result.Result == true ? "Zapisano zmiany." : "Próba edycji nie powiodła się.";
                }
                else
                {
                    result.Message = "Edycja nie powiodła się. Rejon o takiej nazwie już istnieje w tej firmie.";
                }
            }
            else
            {
                if (_RejonDao.SprawdzCzyRejonIstniejeWFirmie(rejonPrzedZmiana, rejonDoEdycji.Firma))
                {
                    if (!_RejonDao.SprawdzCzyRejonIstniejeWFirmie(rejonDoEdycji.Rejon, rejonDoEdycji.Firma))
                    {
                        result.Result = _RejonDao.EdytujRejonDlaFirmy(rejonDoEdycji, rejonPrzedZmiana, idoper, idakcept);
                        result.Message = result.Result == true ? "Zapisano zmiany." : "Próba edycji nie powiodła się.";
                    }
                    else
                    {
                        result.Message = "Edycja nie powiodła się. Rejon o podanej nazwie już istnieje w tej firmie.";
                    }
                }
                else
                {
                    result.Message = "Edycja nie powiodła się. ";
                }
            }
            return result;
        }
        public InsertResult UsunRejon(string firma, string rejon, string idoper, string idakcept)
        {
            InsertResult result = new InsertResult();

            if (_RejonDao.SprawdzCzyRejonIstniejeWFirmie(rejon, firma))
            {
                result.Result = _RejonDao.UsunRejon(firma, rejon, idoper, idakcept);
                result.Message = result.Result == true ? "Rejon został usunięty." : "Operacja usuwania nie powiodła się.";
            }
            return result;
        }
    }
}
