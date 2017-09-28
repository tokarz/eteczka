using System.Collections.Generic;
using Eteczka.Model.Entities;
using Eteczka.Model.DTO;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public interface IPlikiService
    {
        List<Pliki> PobierzWszystkie(string sortOrder = "asc", string sortColumn = "Id");
        List<Pliki> PobierzDlaUzytkownika(string userId, string sortOdred = "asc", string sortColumn = "Id");
        List<Pliki> PobierzZawierajaceTekst(string searchText, string sortOrder = "asc", string sortColumn = "Id");
        List<Pliki> PobierzPlikiDlaFirmy(string firma);
        MetaDanePliku PobierzMetadane(string plik);
        StanPlikow PobierzStanPlikow(string sessionId);
        bool ZakomitujPlikDoBazy(KomitPliku plik, string firma, string idOper);
    }
}
