using Eteczka.BE.DTO;
using Eteczka.Model.DTO;
using Eteczka.Model.Entities;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public interface IKatLoginyService
    {
        KatLoginy GetUserByNameAndPassword(string name, string password);
        List<KatLoginyDetale> GetUserDetails(string identyfikator);
        List<KatLoginy> GetAllUsers();
        List<KatLoginyDetale> GetAllUsersDetails();
        bool UsunFirmeUzytkownika(KatLoginy user, string firma);
        bool DodajNowegoUzytkownika(AddKatLoginyDto user);
        bool ZmienHaslo(AddKatLoginyDto user);
        bool UsunUzytkownika(AddKatLoginyDto user);

    }
}
