using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToPracownikMapper
    {
        Pracownik Map(JToken parsedJson);
    }
}
