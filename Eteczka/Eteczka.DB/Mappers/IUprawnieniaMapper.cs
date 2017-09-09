using Eteczka.DB.Entities;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public interface IUprawnieniaMapper
    {
        Uprawnienia Map(DataRow queryResult);
    }
}
