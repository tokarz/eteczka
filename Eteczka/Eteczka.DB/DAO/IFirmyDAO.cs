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
        
    }
}
