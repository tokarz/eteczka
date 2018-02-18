using Eteczka.BE.DTO;
using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public interface IKatLoginyService
    {
        KatLoginy GetUserByNameAndPassword(string name, string password);
        KatLoginyDetale GetUserDetails(string identyfikator);
        List<KatLoginyFirmy> GetUserCompanies(string identyfikator);

        List<DaneiDetaleUzytkownika> PobierzDaneUzytkownikow();
        List<KatLoginyDetale> GetAllUsersDetails();

        bool UsunFirmeUzytkownika(KatLoginyFirmy firma);
        bool DodajNowegoUzytkownika(AddKatLoginyDto user);
        bool ZmienHaslo(AddKatLoginyDto user);
        bool UsunUzytkownika(string identyfikator);

    }
}
