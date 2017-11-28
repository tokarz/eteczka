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
        List<KatRejony> PobieraczRejonowDlaFirmy(string firma);

    }
}
