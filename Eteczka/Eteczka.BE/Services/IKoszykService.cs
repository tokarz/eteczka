using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.BE.Services
{
    public interface IKoszykService
    {
        int PobierzIloscPlikowWKoszyku(KatLoginyFirmy firma);
        List<Pliki> PobierzPlikiWKoszyku(KatLoginyFirmy firma);
        bool DodajPlikiDoKoszyka(KatLoginyFirmy firma, List<string> plikiId);
        bool UsunZKoszyka(KatLoginyFirmy firma, List<string> plikiId);
        bool WyczyscKoszyk(KatLoginyFirmy firma);
    }
}
