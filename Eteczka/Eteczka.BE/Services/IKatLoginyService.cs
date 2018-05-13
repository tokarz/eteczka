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

        bool DodajNowegoUzytkownika(AddKatLoginyDto user);
        bool EdytujUzytkownika(AddKatLoginyDto user);
        bool DodajFirmeDlaUzytkownika(KatLoginyFirmy firma);
        bool AktualizujFirmeDlaUzytkownika(KatLoginyFirmy firma);
        
        bool ZmienHasloShort(AddKatLoginyDto user);
        bool ZmienHasloAdministratora(string shortPassword, string longPassword);
        bool UsunUzytkownika(string identyfikator);
        bool UsunFirmeUzytkownika(KatLoginyFirmy firma);

        bool SprawdzHasloKrotkie(string id, string haslo);
    }
}
