using Eteczka.BE.DTO;
using Eteczka.Model.Entities;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public interface IKatLoginyService
    {
        KatLoginy GetUserByNameAndPassword(string name, string password);
        List<KatLoginyDetale> GetUserDetails(string identyfikator);
    }
}
