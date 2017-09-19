using Eteczka.Model.Entities;
using Newtonsoft.Json.Linq;

namespace Eteczka.BE.Mappers
{
    public interface IJsonToPodwydzialMapper
    {
        KatPodWydzialy Map(JToken token);
    }
}
