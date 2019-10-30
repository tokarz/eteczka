using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.DAO
{
    public interface IRejonyDAO
    {
        bool ImportujRejony(List<KatRejony> rejony);
        int PoliczRejonyWBazie();
        List<KatRejony> PobieraczRejonow();
        List<KatRejony> PobierzRejonyDlaFirmy(string firma);
        List<KatRejony> PobierzAktywneRejonyDlaFirmy(string firma);
        List<KatRejony> PobierzNieaktywneRejonyDlaFirmy(string firma);
        bool DodajRejonDlaFirmy(KatRejony rejonDoDodania, string idoper, string idakcept);
        bool SprawdzCzyRejonIstniejeWFirmie(string rejon, string firma);
        bool EdytujRejonDlaFirmy(KatRejony rejonDoEdycji,  string idoper, string idakcept);
        bool UsunRejon(string firma, string rejon, string idoper, string idakcept);
        List<KatRejony> WyszukajRejon(string firma, string search);

    }
}
