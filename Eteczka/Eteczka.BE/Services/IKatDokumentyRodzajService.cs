using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.Model.Entities;
using Eteczka.BE.Model;
using Eteczka.Model.DTO;

namespace Eteczka.BE.Services
{
    public interface IKatDokumentyRodzajService
    {
        List<KatDokumentyRodzaj> PobierzRodzDok();
        InsertResult DodajRodzajDokumentuDoBazy(string symbol, string nazwaDokumentu, string typEdycji, string teczkaDzial, SessionDetails sesja);
        InsertResult DezaktywujRodzajDokumentu(string symbol, SessionDetails sesja);
        KatDokumentyRodzaj SzukajRodzajuDokumentuPoSymbolu(string symbol);
    }
}
