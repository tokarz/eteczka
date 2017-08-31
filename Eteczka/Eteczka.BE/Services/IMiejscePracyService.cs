using Eteczka.BE.DTO;
using Eteczka.DB.DTO;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public interface IMiejscePracyService
    {
        List<MiejscePracyDlaPracownikaDto> PobierzMiejscaPracyDlaPracownika(PracownikDTO pracownik);
    }
}
