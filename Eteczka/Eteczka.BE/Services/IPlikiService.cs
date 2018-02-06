using System.Collections.Generic;
using Eteczka.Model.Entities;
using Eteczka.Model.DTO;
using Eteczka.BE.Model;

namespace Eteczka.BE.Services
{
    public interface IPlikiService
    {
        List<Pliki> PobierzWszystkie(string sortOrder = "asc", string sortColumn = "datapocz");
        List<Pliki> PobierzDlaUzytkownika(string numeread, string firma, string sortOdred = "nrdokumentu asc", string sortColumn = "teczkadzial asc,");
        List<Pliki> PobierzZawierajaceTekst(string searchText, string sortOrder = "asc", string sortColumn = "datapocz");
        List<Pliki> PobierzPlikiDlaFirmy(string firma, string sortOrder = "asc", string sortColumn = "datapocz");
        MetaDanePliku PobierzMetadane(string plik);
        StanPlikow PobierzStanPlikow(string sessionId);
        bool ZakomitujPlikDoBazy(KomitPliku plik, string firma, string idOper);
        List<Pliki> SzukajPlikiZFiltrow(SessionDetails sesja, FiltryPlikow filtry, string sortOrder = "asc", string sortColumn = "datapocz");
        bool WyslijPlikiMailem(SessionDetails sesja, string adresaci, List<string> Zalaczniki, string hasloDoZip, string temat, string wiadomosc);
        bool EdytujDokumentWBazie(SessionDetails sesja, KomitPliku plik);
    }
}
