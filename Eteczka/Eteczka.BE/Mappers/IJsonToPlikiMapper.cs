using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToPlikiMapper
    {
        Pliki Map(JToken parsedJson);
    }
}
