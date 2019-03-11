using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.DAO
{
    public interface IFirmyDAO
    {
        bool ImportujFirmy(List<KatFirmy> firmy);
        int PoliczFirmyWBazie();
        List<KatFirmy> PobierzFirmyZBazy(string orderBy = "firma");
        bool ZapiszKatalogRoboczy(string firma, string sciezka);
        bool DodajFirme(KatFirmy firmaDoDodania, string idoper, string idakcept);
        bool EdytujFirme(KatFirmy firmaDoEdycji, string nipPrzedZmiana, string idoper, string idakcept);
        bool DezaktywujFirme(string nip);
        bool PrzywrocFirmeZBazy(string nip);
        KatFirmy WyszukajFirmePoNipie(string nip);
        


    }
}
