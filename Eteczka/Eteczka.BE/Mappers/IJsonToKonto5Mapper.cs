using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToKonto5Mapper
    {
        KatKonto5 Map(JToken token);
    }
}
