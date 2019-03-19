using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eteczka.BE.DTO;
using Eteczka.Model.DTO;
using Eteczka.Model.Entities;

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


    }
}
