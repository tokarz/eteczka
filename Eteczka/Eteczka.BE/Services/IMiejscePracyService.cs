using Eteczka.Model.DTO;
using System.Collections.Generic;
using Eteczka.Model.Entities;

namespace Eteczka.BE.Services
{
    public interface IMiejscePracyService
    {
        List<MiejscePracyDlaPracownika> PobierzMiejscaPracyDlaPracownika(Pracownik pracownik);
    }
}
