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
        List<KatWydzialy> PobierzWydzialyDlaFirmy(SessionDetails sesja);
        InsertResult DodajWydzialDlaFirmy(KatWydzialy wydzialDoDodania, string idoper, string idakcept);
        InsertResult EdytujWydzialDlaFirmy(KatWydzialy wydzialDoEdycji, string idoper, string idakcept);
        InsertResult UsunWydzialZFirmy(KatWydzialy wydzialDoUsuniecia, string idoper, string idakcept);

    }
}
