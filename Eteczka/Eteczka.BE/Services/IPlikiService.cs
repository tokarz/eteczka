using System.Collections.Generic;
using Eteczka.DB.Entities;
using Eteczka.BE.DTO;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public interface IPlikiService
    {
        List<KatTeczki> PobierzWszystkie(string sortOrder = "asc", string sortColumn = "Id");
        List<KatTeczki> PobierzDlaUzytkownika(string userId, string sortOdred = "asc", string sortColumn = "Id");
        List<KatTeczki> PobierzZawierajaceTekst(string searchText, string sortOrder = "asc", string sortColumn = "Id");
        MetaDanePliku PobierzMetadane(string plik);
        StanPlikow PobierzStanPlikow(StanSesji sesja);
    }
}
