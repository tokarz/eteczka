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
    public interface IPodWydzialService
    {
        List<KatPodWydzialy> PobranaListaPodWydzialow(string firma, string wydzial);
        List<KatPodWydzialy> PobierzAktywnePodwydzialyDlaFirmy(string firma, string wydzial);
        List<KatPodWydzialy> PobierzNieaktywnePodwydzialyDlaFirmy(string firma, string wydzial);
        InsertResult DodajPodWydzial(KatPodWydzialy podWydzialDoDodania, string idoper, string idakcept);
        InsertResult EdytujPodWydzial(KatPodWydzialy podWydzialDoEdycji, string idoper, string idakcept);
        InsertResult UsunPodWydzial(KatPodWydzialy podWydzialDoUsuniecia, string idoper, string idakcept);
        InsertResult PrzywrocPodWydzialZBazy(KatPodWydzialy podwydzial, string idoper, string idakcept);
    }
}
