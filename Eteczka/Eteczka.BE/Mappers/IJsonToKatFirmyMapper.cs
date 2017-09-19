using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToKatFirmyMapper
    {
        KatFirmy Map(JToken parsedJson);
    }
}
