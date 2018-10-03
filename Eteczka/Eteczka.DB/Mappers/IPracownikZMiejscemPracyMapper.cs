using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Mappers
{
    public interface IPracownikZMiejscemPracyMapper
    {
        Pracownik MapujDoPracownika(PracownikZMiejscemPracy pracownikDoDodania);
        MiejscePracy MapujDoMiejscaPracy(PracownikZMiejscemPracy miejscePracyDoDodania);
    }
}
