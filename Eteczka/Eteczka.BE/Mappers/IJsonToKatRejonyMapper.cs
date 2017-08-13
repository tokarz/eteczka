using Eteczka.DB.Entities;
using Newtonsoft.Json.Linq;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToKatRejonyMapper
    {
        KatRejony Map(JToken parsedJson);
    }
}
