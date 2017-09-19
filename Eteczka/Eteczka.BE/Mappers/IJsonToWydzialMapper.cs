using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToWydzialMapper
    {
        KatWydzialy Map(JToken token);
    }
}
