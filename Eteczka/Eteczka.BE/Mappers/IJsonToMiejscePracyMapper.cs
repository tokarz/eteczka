using Eteczka.DB.Entities;
using Newtonsoft.Json.Linq;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToMiejscePracyMapper
    {
        MiejscePracy Map(JToken parsedJson);
    }
}
