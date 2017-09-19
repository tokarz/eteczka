using Eteczka.Model.Entities;
using System.Data;

namespace Eteczka.DB.Mappers
{
    public interface IPlikiMapper
    {
        Pliki MapujZSql(DataRow result);
    }
}
