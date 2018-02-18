using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.DAO
{
    public interface IKoszykDAO
    {
        int Policz(KatLoginyFirmy firma);
        List<Pliki> PobierzZawartoscKoszyka(KatLoginyFirmy firma);
        bool DodajPlikiDoKoszyka(KatLoginyFirmy firma, List<string> plikiId);
        bool UsunZKoszyka(KatLoginyFirmy firma, List<string> plikiId);
        bool WyczyscKoszyk(KatLoginyFirmy firma);
    }
}
