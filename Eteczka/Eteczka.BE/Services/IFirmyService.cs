using Eteczka.BE.Model;
using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public interface IFirmyService
    {
        List<KatFirmy> PobierzWszystkie();
        List<KatFirmy> PobierzWszystkieAktywneFirmy();
        List<KatFirmy> PobierzWszystkieNieaktywneFirmy();
        KatFirmy WyszukajFirmePoNipie(string nip);

        InsertResult DodajFirme(KatFirmy firmaDoDodania, string idoper, string idakcept);
        InsertResult EdytujFirme(KatFirmy firmaDoEdycji, string nip, string idoper, string idakcept);
        InsertResult UsunFirme(string nip, string idoper, string idakcept);
        List<KatFirmy> WyszukajFirmePoNipieFirmieLubNazwie(string search);
        InsertResult PrzywrocFirmeZBazy(string nip, string idoper, string idakcept);

    }
}
