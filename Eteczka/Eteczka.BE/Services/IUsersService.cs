using Eteczka.BE.DTO;

namespace Eteczka.BE.Services
{
    public interface IUsersService
    {
        UserDto GetUserByNameAndPassword(string name, string password);
    }
}
