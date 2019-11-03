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
    public interface IWydzialyService
    {
        List<KatWydzialy> PobierzWydzialyDlaFirmy(string firma);
        List<KatWydzialy> PobierzAktywneWydzialyDlaFirmy(string firma);
        List<KatWydzialy> PobierzNieaktywneWydzialyDlaFirmy(string firma);

        InsertResult DodajWydzialDlaFirmy(KatWydzialy wydzialDoDodania, string idoper, string idakcept);
        InsertResult EdytujWydzialDlaFirmy(KatWydzialy wydzialDoEdycji, string idoper, string idakcept);
        InsertResult UsunWydzialZFirmy(KatWydzialy wydzialDoUsuniecia, string idoper, string idakcept);
        InsertResult PrzywrocWydzialWFirmieZDb(string firma, string wydzial, string idoper, string idakcept);

    }
}
