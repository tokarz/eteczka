using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.DB.DAO;
using Eteczka.Model.DTO;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public class KatDokumentyRodzajService : IKatDokumentyRodzajService
    {
        private KatDokumentyRodzajDAO _KatDokumentyRodzajDAO;

        public KatDokumentyRodzajService(KatDokumentyRodzajDAO KatDokumentyRodzajDAO)
        {
            this._KatDokumentyRodzajDAO = KatDokumentyRodzajDAO;
        }

        public List<KatDokumentyRodzaj> PobierzRodzDok()
        {
            List<KatDokumentyRodzaj> PobraneDok = _KatDokumentyRodzajDAO.PobierzWszystkieRodzDok();

            return PobraneDok;
        }
        public InsertResult DodajRodzajDokumentuDoBazy(string symbol, string nazwaDokumentu, string typEdycji, string teczkaDzial, SessionDetails sesja)
        {
            InsertResult result = new InsertResult();
            KatDokumentyRodzaj dokumentWBazie = _KatDokumentyRodzajDAO.ZnajdzRodzajDokumentuPoSymbolu(symbol);
            if (dokumentWBazie == null)
            {
               result.Result = _KatDokumentyRodzajDAO.DodajRodzajDokumentu(symbol, nazwaDokumentu, typEdycji, teczkaDzial, sesja.AktywnyUser.Identyfikator, sesja.AktywnyUser.Identyfikator);
               result.Message = "Rodzaj dokumentu został dopisany do bazy.";
            }
            else
            {
                result.Result = false;
                result.Message = "Rodzaj dokumentu o takim symbolu istnieje! Podaj inny symbol.";
            }

            return result;
        }

        public InsertResult DezaktywujRodzajDokumentu(string symbol, SessionDetails sesja)
        {
            InsertResult result = new InsertResult();
            KatDokumentyRodzaj dokumentWBazie = _KatDokumentyRodzajDAO.ZnajdzRodzajDokumentuPoSymbolu(symbol);
            if (dokumentWBazie != null)
            {
                result.Result = _KatDokumentyRodzajDAO.DeaktywujRodzajuDokumentu(symbol, sesja.AktywnyUser.Identyfikator.Trim(), sesja.AktywnyUser.Identyfikator.Trim());
                result.Message = "Rodzaj dokumentu został usunięty.";
            }
            else
            {
                result.Result = false;
                result.Message = "Rodzaj dokumentu o takim symbolu nie istnieje! Podaj inny symbol.";
            }

            return result;
        }
        public KatDokumentyRodzaj SzukajRodzajuDokumentuPoSymbolu(string symbol)
        {
            KatDokumentyRodzaj dokument = _KatDokumentyRodzajDAO.ZnajdzRodzajDokumentuPoSymbolu(symbol);
            return dokument;
        }
    }
}
