using Eteczka.BE.DTO;
using System.Collections.Generic;

namespace Eteczka.BE.Services
{
    public interface IUsersService
    {
        UserDto GetUserByNameAndPassword(string name, string password);
    }
}
