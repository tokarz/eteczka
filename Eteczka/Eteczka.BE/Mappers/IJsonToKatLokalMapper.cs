using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToKatLokalMapper
    {
        KatLokalPapier Map(JToken parsedJson);
    }
}


