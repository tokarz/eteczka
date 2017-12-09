using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToKatDokumentyRodzajMapper
    {
        KatDokumentyRodzaj Map(JToken parsedJson);
    }
}
