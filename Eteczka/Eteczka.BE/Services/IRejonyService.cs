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
    public interface IRejonyService
    {
        List<KatRejony> PobierzRejony();
        List<KatRejony> PobierzRejonyDlaFirmy(SessionDetails sesja);
        InsertResult DodajRejonDlaFirmy(KatRejony rejonDoDodania, string idoper, string idakcept);
        InsertResult EdytujRejonDlaFirmy(KatRejony rejonDoEdycji, string rejonPrzedZmiana, string idoper, string idakcept);
        InsertResult UsunRejon(string firma, string rejon, string idoper, string idakcept);
    }
}
