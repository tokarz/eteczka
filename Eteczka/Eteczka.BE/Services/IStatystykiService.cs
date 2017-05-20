using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.BE.Enums;

namespace Eteczka.BE.Services
{
    public interface IStatystykiService
    {
        List<DaneWykresowe> PobierzDaneWykresowe(TypWykresu typWykresu);
    }
}
