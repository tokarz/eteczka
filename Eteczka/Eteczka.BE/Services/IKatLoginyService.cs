using Eteczka.BE.DTO;
using Eteczka.DB.Entities;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public interface IKatLoginyService
    {
        KatLoginy GetUserByNameAndPassword(string name, string password);
        KatLoginyDetale GetUserDetails(string identyfikator);
    }
}
